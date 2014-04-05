using System;
using System.Threading.Tasks;
using ChatterBox.MessageContracts.Requests;
using Nimbus.Handlers;

namespace ChatterBox.ChatServer.Handlers
{
    public class GetUserInfoRequestHandler : IHandleRequest<GetUserInfoRequest, GetUserInfoResponse>
    {
        public Task<GetUserInfoResponse> Handle(GetUserInfoRequest request)
        {
            throw new NotImplementedException();
        }
    }
}