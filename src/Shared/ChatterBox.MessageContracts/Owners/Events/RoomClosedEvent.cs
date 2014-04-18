using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Owners.Events
{
    public class RoomClosedEvent : IBusEvent
    {
        public RoomClosedEvent()
        {   
        }
        
        public RoomClosedEvent(Guid roomId, string roomName)
        {
            if (roomId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "roomId");
            
            if (roomName == null) 
                throw new ArgumentNullException("roomName");

            RoomId = roomId;
            RoomName = roomName;
        }

        public Guid RoomId { get; private set; }
        
        public string RoomName { get; private set; }
    }
}