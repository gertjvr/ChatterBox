using System;
using ChatterBox.Core.Infrastructure.Facts;

namespace Domain.Aggregates.ConversationAggregate.Facts
{
    public class ConversationCreatedFact : FactAbout<Conversation>
    {
        public string Topic { get; set; }
        
        public Guid OwnerId { get; set; }
        
        public Guid[] Contacts { get; set; }
    }
}