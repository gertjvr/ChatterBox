using System.Collections.Generic;
using ChatterBox.MessageContracts.Dtos;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Requests
{
    public class PreviousMessagesResponse : IBusResponse
    {
        protected PreviousMessagesResponse()
        {   
        }

        public PreviousMessagesResponse(IEnumerable<MessageDto> messages)
        {
            Messages = messages;
        }

        public IEnumerable<MessageDto> Messages { get; protected set; }
    }
}