using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Events
{
    public class AdminAddedEvent : IBusEvent
    {
        public AdminAddedEvent(Guid userId, string userName)
        {
            UserId = userId;
            UserName = userName;
        }

        public Guid UserId { get; protected set; }

        public string UserName { get; protected set; }
    }
}