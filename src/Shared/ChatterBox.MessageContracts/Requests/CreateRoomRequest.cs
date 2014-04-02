using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Requests
{
    public class CreateRoomRequest : IBusRequest<CreateRoomRequest, CreateRoomResponse>
    {
        protected CreateRoomRequest()
        {
        }

        public CreateRoomRequest(Guid userId, string roomName)
        {
            UserId = userId;
            RoomName = roomName;
        }

        public Guid UserId { get; set; }

        public string RoomName { get; set; }
    };
}