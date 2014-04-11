using System;
using System.Threading.Tasks;
using Autofac.Features.OwnedInstances;
using ChatterBox.Core.Persistence;
using ChatterBox.MessageContracts.Requests;

namespace ChatterBox.ChatServer.Handlers
{
    public class UserInfoRequestHandler : ScopedRequestHandler<UserInfoRequest, UserInfoResponse>
    {
        public UserInfoRequestHandler(Func<Owned<IUnitOfWork>> unitOfWork) : base(unitOfWork)
        {
        }

        public override Task<UserInfoResponse> Execute(IUnitOfWork context, UserInfoRequest request)
        {
            throw new NotImplementedException();
        }
    }
}