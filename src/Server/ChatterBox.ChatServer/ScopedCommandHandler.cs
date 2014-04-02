using System;
using System.Threading.Tasks;
using Autofac.Features.OwnedInstances;
using ChatterBox.Core.Persistence;
using Nimbus.Handlers;
using Nimbus.MessageContracts;

namespace ChatterBox.ChatServer
{
    public abstract class ScopedCommandHandler<TBusCommand> : IHandleCommand<TBusCommand> where TBusCommand : IBusCommand
    {
        private readonly Func<Owned<IUnitOfWork>> _unitOfWork;

        public ScopedCommandHandler(Func<Owned<IUnitOfWork>> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(TBusCommand command)
        {
            using (var unitOfWork = _unitOfWork())
            {
                var context = unitOfWork.Value;

                try
                {
                    await Execute(context, command);
                }
                catch
                {
                    context.Abandon();
                    throw;
                }
            }
        }

        public abstract Task Execute(IUnitOfWork context, TBusCommand command);
    }
}