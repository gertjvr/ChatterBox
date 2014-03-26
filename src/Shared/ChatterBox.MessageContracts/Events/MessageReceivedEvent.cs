using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Events
{
    public class MessageReceivedEvent : IBusEvent
    {
        public Guid FromId { get; set; }

        public string Message { get; set; }

        protected MessageReceivedEvent()
        {

        }

        public MessageReceivedEvent(Guid fromId, string message)
        {
            FromId = fromId;
            Message = message;
        }
    }
}