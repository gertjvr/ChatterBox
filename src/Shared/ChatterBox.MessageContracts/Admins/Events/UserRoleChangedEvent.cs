using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Admins.Events
{
    public class UserRoleChangedEvent : IBusEvent
    {
        protected UserRoleChangedEvent()
        {   
        }

        public UserRoleChangedEvent(Guid userId, string userName, int userRole)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "userId");

            if (userName == null) 
                throw new ArgumentNullException("userName");

            UserId = userId;
            UserName = userName;
            UserRole = userRole;
        }

        public Guid UserId { get; private set; }

        public string UserName { get; private set; }

        public int UserRole { get; private set; }
    }
}