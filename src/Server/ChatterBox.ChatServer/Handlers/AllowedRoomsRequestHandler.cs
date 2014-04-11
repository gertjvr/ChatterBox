using System;
using System.Threading.Tasks;
using Autofac.Features.OwnedInstances;
using ChatterBox.Core.Persistence;
using ChatterBox.MessageContracts.Requests;

namespace ChatterBox.ChatServer.Handlers
{
    public class AllowedRoomsRequestHandler : ScopedRequestHandler<AllowedRoomsRequest, AllowedRoomsResponse>
    {
        public AllowedRoomsRequestHandler(Func<Owned<IUnitOfWork>> unitOfWork) : base(unitOfWork)
        {
        }

        public override Task<AllowedRoomsResponse> Execute(IUnitOfWork context, AllowedRoomsRequest request)
        {
            throw new NotImplementedException();
        }
    }
}