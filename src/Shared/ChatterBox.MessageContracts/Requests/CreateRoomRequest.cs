using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Requests
{
    public class CreateRoomRequest : IBusRequest<CreateRoomRequest, CreateRoomResponse>
    {
        protected CreateRoomRequest()
        {   
        }

        public CreateRoomRequest(string roomName, Guid userId)
        {
            RoomName = roomName;
            UserId = userId;
        }

        public string RoomName { get; protected set; }

        public Guid UserId { get; protected set; }
    };
}