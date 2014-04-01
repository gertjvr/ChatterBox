using System.Threading.Tasks;
using ChatterBox.MessageContracts.Rooms.Requests;
using Nimbus.Handlers;

namespace ChatterBox.ChatServer.Handlers.Rooms
{
    public class CreateRoomRequestHandler : IHandleRequest<CreateRoomRequest, CreateRoomResponse>
    {
        public Task<CreateRoomResponse> Handle(CreateRoomRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}