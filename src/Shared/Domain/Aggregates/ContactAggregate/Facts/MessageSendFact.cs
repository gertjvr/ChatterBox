using System;
using ChatterBox.Core.Infrastructure.Facts;

namespace Domain.Aggregates.ContactAggregate.Facts
{
    public class MessageSendFact : FactAbout<Contact>
    {
        protected MessageSendFact()
        {
            
        }

        public MessageSendFact(Guid aggregateRootId, Guid receiverId, string message)
            : base(aggregateRootId)
        {
            ReceiverId = receiverId;
            Message = message;
        }

        public Guid ReceiverId { get; protected set; }

        public string Message { get; protected set; }
    }
}