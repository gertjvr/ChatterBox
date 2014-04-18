using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Users.Commands
{
    public class UpdateActivityCommand : IBusCommand
    {
        protected UpdateActivityCommand()
        {   
        }
        
        public UpdateActivityCommand(Guid userId, Guid clientId, string userAgent)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "userId");
            
            if (clientId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "clientId");
            
            if (userAgent == null) 
                throw new ArgumentNullException("userAgent");

            UserId = userId;
            ClientId = clientId;
            UserAgent = userAgent;
        }

        public Guid UserId { get; private set; }

        public Guid ClientId { get; private set; }
        
        public string UserAgent  { get; private set; }
    }
}