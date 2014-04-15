using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Events
{
    public class UserAllowedEvent : IBusEvent
    {
        public UserAllowedEvent(
            Guid userId,
            string userName,
            Guid roomId,
            string roomName)
        {
            UserId = userId;
            UserName = userName;
            RoomId = roomId;
            RoomName = roomName;
        }

        public Guid UserId { get; protected set; }
        
        public string UserName { get; protected set; }
        
        public Guid RoomId { get; protected set; }
        
        public string RoomName { get; protected set; }
    }
}