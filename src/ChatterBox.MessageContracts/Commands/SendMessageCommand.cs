using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Commands
{
    public class SendMessageCommand : IBusCommand
    {
        public Guid ClientId { get; set; }

        public string Message { get; set; }

        protected SendMessageCommand()
        {

        }

        public SendMessageCommand(Guid clientId, string message)
        {
            Message = message;
            ClientId = clientId;
        }
    }
}