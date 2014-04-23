using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Users.Commands
{
    public class ConnectClientCommand : IBusCommand
    {
        protected ConnectClientCommand()
        {   
        }
        
        public ConnectClientCommand(Guid clientId, string userAgent, Guid callingUserId)
        {
            if (callingUserId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "callingUserId");
            
            if (userAgent == null) 
                throw new ArgumentNullException("userAgent");

            if (clientId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "clientId");

            ClientId = clientId;
            CallingUserId = callingUserId;
            UserAgent = userAgent;
        }

        public Guid ClientId { get; private set; }

        public string UserAgent { get; private set; }

        public Guid CallingUserId { get; private set; }
    }
}