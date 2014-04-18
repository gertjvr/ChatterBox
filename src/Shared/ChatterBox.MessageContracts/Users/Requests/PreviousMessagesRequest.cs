using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Users.Requests
{
    public class PreviousMessagesRequest : IBusRequest<PreviousMessagesRequest, PreviousMessagesResponse>
    {
        protected PreviousMessagesRequest()
        {   
        }
        
        public PreviousMessagesRequest(Guid fromId, int numberOfMessages)
        {
            if (fromId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "fromId");

            FromId = fromId;
            NumberOfMessages = numberOfMessages;
        }

        public Guid FromId { get; private set; }

        public int NumberOfMessages { get; private set; }
    }
}