using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Users.Commands
{
    public class DisconnectClientCommand : IBusCommand
    {
        protected DisconnectClientCommand()
        {   
        }
        
        public DisconnectClientCommand(Guid clientId, Guid callingUserId)
        {
            if (clientId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "clientId");

            if (callingUserId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "callingUserId");

            ClientId = clientId;
            CallingUserId = callingUserId;
        }

        public Guid ClientId { get; private set; }

        public Guid CallingUserId { get; private set; }
    }
}