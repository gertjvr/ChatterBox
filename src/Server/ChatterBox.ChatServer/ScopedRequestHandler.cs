using System;
using System.Threading.Tasks;
using Autofac.Features.OwnedInstances;
using ChatterBox.Core.Persistence;
using Nimbus.Handlers;
using Nimbus.MessageContracts;

namespace ChatterBox.ChatServer
{
    public abstract class ScopedRequestHandler<TBusRequest, TBusResponse> : IHandleRequest<TBusRequest, TBusResponse>
        where TBusRequest : IBusRequest<TBusRequest, TBusResponse>
        where TBusResponse : IBusResponse
    {
        private readonly Func<Owned<IUnitOfWork>> _unitOfWork;

        protected ScopedRequestHandler(Func<Owned<IUnitOfWork>> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TBusResponse> Handle(TBusRequest request)
        {
            using (var unitOfWork = _unitOfWork())
            {
                var context = unitOfWork.Value;

                try
                {
                    return await Execute(context, request);
                }
                catch
                {
                    context.Abandon();
                    throw;
                }
            }
        }

        public abstract Task<TBusResponse> Execute(IUnitOfWork context, TBusRequest request);
    }
}