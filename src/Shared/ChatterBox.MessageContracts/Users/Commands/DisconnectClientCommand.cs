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
            ClientId = clientId;
        }

        public Guid ClientId { get; set; }
    }
}