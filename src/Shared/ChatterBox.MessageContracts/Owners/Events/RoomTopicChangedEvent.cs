using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Owners.Events
{
    public class RoomTopicChangedEvent : IBusEvent
    {
        protected RoomTopicChangedEvent()
        {   
        }

        public RoomTopicChangedEvent(Guid roomId, string roomName, string newTopic)
        {
            if (roomId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "roomId");
            
            if (roomName == null) 
                throw new ArgumentNullException("roomName");
            
            if (newTopic == null) 
                throw new ArgumentNullException("newTopic");

            RoomId = roomId;
            RoomName = roomName;
            NewTopic = newTopic;
        }

        public Guid RoomId { get; private set; }
        
        public string RoomName { get; private set; }

        public string NewTopic { get; private set; }
    }
}