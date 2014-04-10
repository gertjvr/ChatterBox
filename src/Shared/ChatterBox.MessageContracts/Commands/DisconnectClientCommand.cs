using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Commands
{
    public class DisconnectClientCommand : IBusCommand
    {
        protected DisconnectClientCommand()
        {   
        }
        
        public DisconnectClientCommand(Guid clientId)
        {
            ClientId = clientId;
        }

        public Guid ClientId { get; protected set; }
    }
}