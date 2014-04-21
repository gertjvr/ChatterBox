using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Users.Requests
{
    public class PreviousMessagesRequest : IBusRequest<PreviousMessagesRequest, PreviousMessagesResponse>
    {
        protected PreviousMessagesRequest()
        {   
        }
        
        public PreviousMessagesRequest(Guid targetUserId, Guid fromId, int numberOfMessages, Guid callingUserId)
        {
            if (targetUserId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "targetUserId");

            if (fromId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "fromId");

            if (callingUserId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "callingUserId");

            TargetUserId = targetUserId;
            FromId = fromId;
            NumberOfMessages = numberOfMessages;
            CallingUserId = callingUserId;
        }

        public Guid TargetUserId { get; private set; }

        public Guid FromId { get; private set; }

        public int NumberOfMessages { get; private set; }

        public Guid CallingUserId { get; private set; }
    }
}