using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Admins.Events
{
    public class AdminAddedEvent : IBusEvent
    {
        protected AdminAddedEvent()
        {   
        }

        public AdminAddedEvent(Guid userId, string userName)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "userId");
            
            if (userName == null) 
                throw new ArgumentNullException("userName");

            UserId = userId;
            UserName = userName;
        }

        public Guid UserId { get; private set; }

        public string UserName { get; private set; }
    }
}