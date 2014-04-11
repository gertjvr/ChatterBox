using System;
using ChatterBox.Core.Infrastructure.Facts;

namespace ChatterBox.Domain.Aggregates.RoomAggregate.Facts
{
    [Serializable]
    public class RoomTopicChangedFact : FactAbout<Room>
    {
        public RoomTopicChangedFact(
            Guid aggregateRootId, 
            string newTopic, 
            Guid userId, 
            DateTimeOffset changedAt) 
            : base(aggregateRootId)
        {
            NewTopic = newTopic;
            UserId = userId;
            ChangedAt = changedAt;
        }

        public string NewTopic { get; protected set; }

        public Guid UserId { get; protected set; }

        public DateTimeOffset ChangedAt { get; protected set; }
    }
}