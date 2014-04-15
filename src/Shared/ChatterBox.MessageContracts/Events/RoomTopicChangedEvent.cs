using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Events
{
    public class RoomTopicChangedEvent : IBusEvent
    {
        public RoomTopicChangedEvent(Guid roomId, string roomName, string newTopic)
        {
            RoomId = roomId;
            RoomName = roomName;
            NewTopic = newTopic;
        }

        public Guid RoomId { get; protected set; }
        
        public string RoomName { get; protected set; }

        public string NewTopic { get; protected set; }
    }
}