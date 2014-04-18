using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Rooms.Requests
{
    public class CreateRoomRequest : IBusRequest<CreateRoomRequest, CreateRoomResponse>
    {
        protected CreateRoomRequest()
        {   
        }

        public CreateRoomRequest(string roomName, Guid userId)
        {
            if (roomName == null)
                throw new ArgumentNullException("roomName");
            
            if (userId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "userId");

            RoomName = roomName;
            UserId = userId;
        }

        public string RoomName { get; private set; }

        public Guid UserId { get; private set; }
    };
}