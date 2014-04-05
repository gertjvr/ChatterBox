using System.Collections.Generic;
using ChatterBox.MessageContracts.Dtos;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Requests
{
    public class GetPreviousMessagesResponse : IBusResponse
    {
        protected GetPreviousMessagesResponse()
        {
        }

        public GetPreviousMessagesResponse(IEnumerable<MessageDto> messages)
        {
            Messages = messages;
        }

        public IEnumerable<MessageDto> Messages { get; set; }
    }
}