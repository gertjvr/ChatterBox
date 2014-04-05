using System;
using System.Threading.Tasks;
using ChatterBox.MessageContracts.Requests;
using Nimbus.Handlers;

namespace ChatterBox.ChatServer.Handlers
{
    public class GetRoomInfoReuestHandler : IHandleRequest<GetRoomInfoRequest, GetRoomInfoResponse>
    {
        public Task<GetRoomInfoResponse> Handle(GetRoomInfoRequest request)
        {
            throw new NotImplementedException();
        }
    }
}