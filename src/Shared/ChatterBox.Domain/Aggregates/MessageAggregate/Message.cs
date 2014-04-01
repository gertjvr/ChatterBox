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

        public static Message Create(Guid roomId, Guid userId, string content, DateTimeOffset createdAt)
        {
            var fact = new MessageCreatedFact
            {
                AggregateRootId = Guid.NewGuid(),
                RoomId = roomId,
                UserId = userId,
                Content = content,
                CreatedAt = createdAt,
            };

            var message = new Message();
            message.Append(fact);
            message.Apply(fact);
            return message;
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