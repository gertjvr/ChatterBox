using System;
using System.Threading.Tasks;
using ChatterBox.MessageContracts.Requests;
using Nimbus.Handlers;

namespace ChatterBox.ChatServer.Handlers
{
    public class GetAllowedRoomsRequestHandler : IHandleRequest<GetAllowedRoomsRequest, GetAllowedRoomsResponse>
    {
        public Task<GetAllowedRoomsResponse> Handle(GetAllowedRoomsRequest request)
        {
            throw new NotImplementedException();
        }
    }
}