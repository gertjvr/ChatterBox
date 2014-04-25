using System;
using ChatterBox.Core.Infrastructure.Entities;
using ChatterBox.Domain.Aggregates.MessageAggregate.Facts;
using ChatterBox.Domain.Properties;

namespace ChatterBox.Domain.Aggregates.MessageAggregate
{
    [Serializable]
    public class Message : AggregateRoot
    {
        protected Message()
        {
        }

        public Message(Guid roomId, Guid userId, string content, DateTimeOffset createdAt) 
            : this(Guid.NewGuid(), roomId, userId, content, createdAt)
        {
        }

        public Message(Guid id, Guid roomId, Guid userId, string content, DateTimeOffset createdAt)
        {
            if (id == null)
                throw new ArgumentException(LanguageResources.GuidCannotBeEmpty, "id");

            if (roomId == Guid.Empty)
                throw new ArgumentException(LanguageResources.GuidCannotBeEmpty, "roomId");

            if (userId == Guid.Empty)
                throw new ArgumentException(LanguageResources.GuidCannotBeEmpty, "userId");

            if (content == null)
                throw new ArgumentNullException("content");

            var fact = new MessageCreatedFact(
                id,
                userId,
                roomId,
                content,
                createdAt);

            Append(fact);
            Apply(fact);
        }

        public Guid RoomId { get; private set; }

        public Guid UserId { get; private set; }

        public string Content { get; private set; }

        public DateTimeOffset CreatedAt { get; private set; }

        public void Apply(MessageCreatedFact fact)
        {
            if (fact == null)
                throw new ArgumentNullException("fact");

            Id = fact.AggregateRootId;
            RoomId = fact.RoomId;
            UserId = fact.UserId;
            Content = fact.Content;
            CreatedAt = fact.CreatedAt;
        }
    }
}