using System;
using System.Threading.Tasks;
using Autofac.Features.OwnedInstances;
using ChatterBox.Core.Persistence;
using ChatterBox.MessageContracts.Requests;

namespace ChatterBox.ChatServer.Handlers
{
    public class PreviousMessagesRequestHandler : ScopedRequestHandler<PreviousMessagesRequest, PreviousMessagesResponse>
    {
        public PreviousMessagesRequestHandler(Func<Owned<IUnitOfWork>> unitOfWork) : base(unitOfWork)
        {
        }

        public override Task<PreviousMessagesResponse> Execute(IUnitOfWork context, PreviousMessagesRequest request)
        {
            throw new NotImplementedException();
        }
    }
}