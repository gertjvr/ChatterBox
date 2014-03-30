using System;
using ChatterBox.Core.Infrastructure.Facts;

namespace Domain.Aggregates.ConversationAggregate.Facts
{
    public class MessageReceivedFact : FactAbout<Conversation>
    {
        public Guid ContactId { get; set; }

        public string Content { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
    }
}