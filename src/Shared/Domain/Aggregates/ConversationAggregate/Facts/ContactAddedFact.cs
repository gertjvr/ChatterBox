using System;
using ChatterBox.Core.Infrastructure.Facts;

namespace Domain.Aggregates.ConversationAggregate.Facts
{
    public class ContactAddedFact : FactAbout<Conversation>
    {
        public Guid ContactId { get; set; }
    }
}