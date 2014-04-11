using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Requests
{
    public class PreviousMessagesRequest : IBusRequest<PreviousMessagesRequest, PreviousMessagesResponse>
    {
        protected PreviousMessagesRequest()
        {   
        }

        public PreviousMessagesRequest(Guid fromId)
        {
            FromId = fromId;
        }

        public Guid FromId { get; protected set; }
    }
}