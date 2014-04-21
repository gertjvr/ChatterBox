using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Users.Commands
{
    public class UpdateActivityCommand : IBusCommand
    {
        protected UpdateActivityCommand()
        {   
        }
        
        public UpdateActivityCommand(Guid targetUserId, Guid clientId, string userAgent, Guid callingUserId)
        {
            if (targetUserId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "targetUserId");
            
            if (clientId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "clientId");
            
            if (userAgent == null) 
                throw new ArgumentNullException("userAgent");

            if (callingUserId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "callingUserId");

            TargetUserId = targetUserId;
            ClientId = clientId;
            UserAgent = userAgent;
            CallingUserId = callingUserId;
        }

        public Guid TargetUserId { get; private set; }

        public Guid ClientId { get; private set; }
        
        public string UserAgent  { get; private set; }

        public Guid CallingUserId { get; private set; }
    }
}