using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Events
{
    public class UserRoleChangedEvent : IBusEvent
    {
        public UserRoleChangedEvent(Guid userId, string userName, int userRole)
        {
            UserId = userId;
            UserName = userName;
            UserRole = userRole;
        }

        public Guid UserId { get; protected set; }

        public string UserName { get; protected set; }

        public int UserRole { get; protected set; }
    }
}