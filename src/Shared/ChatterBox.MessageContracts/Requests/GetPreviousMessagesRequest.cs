using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Requests
{
    public class GetPreviousMessagesRequest : IBusRequest<GetPreviousMessagesRequest, GetPreviousMessagesResponse>
    {
        protected GetPreviousMessagesRequest()
        {
        }

        public GetPreviousMessagesRequest(Guid fromId)
        {
            FromId = fromId;
        }

        public Guid FromId { get; set; }
    }
}