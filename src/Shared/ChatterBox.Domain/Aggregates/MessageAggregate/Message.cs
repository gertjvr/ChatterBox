using System;
using ChatterBox.Core.Infrastructure.Entities;
using ChatterBox.Domain.Aggregates.MessageAggregate.Facts;

namespace ChatterBox.Domain.Aggregates.MessageAggregate
{
    [Serializable]
    public class Message : AggregateRoot
    {
        protected Message()
        {   
        }

        public Message(Guid roomId, Guid userId, string content, DateTimeOffset createdAt)
        {
            var fact = new MessageCreatedFact(
                Guid.NewGuid(),
                roomId,
                userId,
                content,
                createdAt);

            Append(fact);
            Apply(fact);
        }

        public Guid RoomId { get; protected set; }

        public Guid UserId { get; protected set; }

        public string Content { get; protected set; }

        public DateTimeOffset CreatedAt { get; protected set; }

        public void Apply(MessageCreatedFact fact)
        {
            Id = fact.AggregateRootId;
            RoomId = fact.RoomId;
            UserId = fact.UserId;
            Content = fact.Content;
            CreatedAt = fact.CreatedAt;
        }
    }
}