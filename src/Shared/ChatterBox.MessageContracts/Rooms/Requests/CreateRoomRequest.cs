using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Rooms.Requests
{
    public class CreateRoomRequest : IBusRequest<CreateRoomRequest, CreateRoomResponse>
    {
        protected CreateRoomRequest()
        {   
        }

        public CreateRoomRequest(string roomName, Guid callingUserId)
        {
            if (roomName == null)
                throw new ArgumentNullException("roomName");
            
            if (callingUserId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "callingUserId");

            RoomName = roomName;
            CallingUserId = callingUserId;
        }

        public string RoomName { get; private set; }

        public Guid CallingUserId { get; private set; }
    };
}