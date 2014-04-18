using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Users.Commands
{
    public class DisconnectClientCommand : IBusCommand
    {
        protected DisconnectClientCommand()
        {   
        }
        
        public DisconnectClientCommand(Guid clientId)
        {
            if (clientId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "clientId");

            ClientId = clientId;
        }

        public Guid ClientId { get; private set; }
    }
}