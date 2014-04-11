using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Requests
{
    public class PreviousMessagesRequest : IBusRequest<PreviousMessagesRequest, PreviousMessagesResponse>
    {
        protected PreviousMessagesRequest()
        {   
        }

        public PreviousMessagesRequest(Guid fromId, int numberOfMessages)
        {
            FromId = fromId;
            NumberOfMessages = numberOfMessages;
        }

        public Guid FromId { get; protected set; }

        public int NumberOfMessages { get; protected set; }
    }
}