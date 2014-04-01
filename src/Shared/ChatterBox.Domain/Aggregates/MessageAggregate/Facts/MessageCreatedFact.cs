using System;
using ChatterBox.Core.Infrastructure.Facts;

namespace ChatterBox.Domain.Aggregates.MessageAggregate.Facts
{
    [Serializable]
    public class MessageCreatedFact : FactAbout<Message>
    {
        public Guid UserId { get; set; }
        
        public Guid RoomId { get; set; }
        
        public string Content { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
    }
}