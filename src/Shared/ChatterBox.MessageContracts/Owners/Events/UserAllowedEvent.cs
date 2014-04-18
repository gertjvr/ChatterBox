using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Owners.Events
{
    public class UserAllowedEvent : IBusEvent
    {
        protected UserAllowedEvent()
        {   
        }

        public UserAllowedEvent(Guid userId, string userName, Guid roomId, string roomName)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "userId");

            if (userName == null)
                throw new ArgumentNullException("userName");

            if (roomId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "roomId");

            if (roomName == null)
                throw new ArgumentNullException("roomName");

            UserId = userId;
            UserName = userName;
            RoomId = roomId;
            RoomName = roomName;
        }

        public Guid UserId { get; private set; }
        
        public string UserName { get; private set; }
        
        public Guid RoomId { get; private set; }
        
        public string RoomName { get; private set; }
    }
}