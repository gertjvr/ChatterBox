using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Commands
{
    public class DisconnectCommand : IBusCommand
    {
        public Guid ClientId { get; set; }

        protected DisconnectCommand()
        {
            
        }

        public DisconnectCommand(Guid clientId)
        {
            ClientId = clientId;
        }
    }
}
