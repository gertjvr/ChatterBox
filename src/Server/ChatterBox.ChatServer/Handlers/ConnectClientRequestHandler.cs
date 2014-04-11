using System;
using System.Threading.Tasks;
using Autofac.Features.OwnedInstances;
using ChatterBox.Core.Persistence;
using ChatterBox.MessageContracts.Requests;

namespace ChatterBox.ChatServer.Handlers
{
    public class ConnectClientRequestHandler : ScopedRequestHandler<ConnectClientRequest, ConnectClientResponse>
    {
        public ConnectClientRequestHandler(Func<Owned<IUnitOfWork>> unitOfWork)
            : base(unitOfWork)
        {
        }

        public override async Task<ConnectClientResponse> Execute(IUnitOfWork context, ConnectClientRequest request)
        {
            return new ConnectClientResponse(Guid.NewGuid());
        }
    }
}