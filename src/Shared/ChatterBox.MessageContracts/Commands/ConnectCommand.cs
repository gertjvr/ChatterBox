using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Commands
{
    public class ConnectCommand : IBusCommand
    {
        public Guid ClientId { get; set; }

        protected ConnectCommand()
        {
            
        }

        public ConnectCommand(Guid clientId)
        {
            ClientId = clientId;
        }
    }
}
