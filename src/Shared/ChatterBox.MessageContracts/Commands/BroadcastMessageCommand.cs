using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Commands
{
    public class BroadcastMessageCommand : IBusCommand
    {
        public Guid ClientId { get; set; }

        public string Message { get; set; }

        protected BroadcastMessageCommand()
        {

        }

        public BroadcastMessageCommand(Guid clientId, string message)
        {
            ClientId = clientId;
            Message = message;
        }
    }
}