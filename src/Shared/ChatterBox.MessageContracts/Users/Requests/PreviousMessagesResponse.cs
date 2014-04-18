using System;
using System.Collections.Generic;
using ChatterBox.MessageContracts.Dtos;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Users.Requests
{
    public class PreviousMessagesResponse : IBusResponse
    {
        protected PreviousMessagesResponse()
        {   
        }

        public PreviousMessagesResponse(IEnumerable<MessageDto> messages)
        {
            if (messages == null) 
                throw new ArgumentNullException("messages");

            Messages = messages;
        }

        public IEnumerable<MessageDto> Messages { get; private set; }
    }
}