using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Events
{
    public class MessageReceivedEvent : IBusEvent
    {
        private readonly Guid _fromId;
        private readonly string _message;

        public MessageReceivedEvent()
        {
        }

        public MessageReceivedEvent(Guid fromId, string message)
        {
            _fromId = fromId;
            _message = message;
        }
    }
}