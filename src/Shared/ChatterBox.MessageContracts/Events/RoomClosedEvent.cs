using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Events
{
    public class RoomClosedEvent : IBusEvent
    {
        public RoomClosedEvent(Guid roomId, string roomName)
        {
            RoomId = roomId;
            RoomName = roomName;
        }

        public Guid RoomId { get; protected set; }
        
        public string RoomName { get; protected set; }
    }
}