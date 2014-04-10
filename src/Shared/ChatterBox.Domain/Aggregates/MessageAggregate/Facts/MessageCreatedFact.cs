using System;
using ChatterBox.Core.Infrastructure.Facts;

namespace ChatterBox.Domain.Aggregates.MessageAggregate.Facts
{
    [Serializable]
    public class MessageCreatedFact : FactAbout<Message>
    {
        public MessageCreatedFact(
            Guid aggregateRootId,
            Guid userId,
            Guid roomId,
            string content,
            DateTimeOffset createdAt)
            : base(aggregateRootId)
        {
            UserId = userId;
            RoomId = roomId;
            Content = content;
            CreatedAt = createdAt;
        }

        public Guid UserId { get; protected set; }

        public Guid RoomId { get; protected set; }

        public string Content { get; protected set; }

        public DateTimeOffset CreatedAt { get; protected set; }
    }
}