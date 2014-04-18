using System;
using ChatterBox.Core.Infrastructure.Facts;
using ChatterBox.Domain.Properties;

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
            if (userId == Guid.Empty)
                throw new ArgumentException(LanguageResources.GuidCannotBeEmpty, "userId");

            if (roomId == Guid.Empty)
                throw new ArgumentException(LanguageResources.GuidCannotBeEmpty, "userId");

            if (content == null)
                throw new ArgumentNullException("content");

            UserId = userId;
            RoomId = roomId;
            Content = content;
            CreatedAt = createdAt;
        }

        public Guid UserId { get; private set; }

        public Guid RoomId { get; private set; }

        public string Content { get; private set; }

        public DateTimeOffset CreatedAt { get; private set; }
    }
}