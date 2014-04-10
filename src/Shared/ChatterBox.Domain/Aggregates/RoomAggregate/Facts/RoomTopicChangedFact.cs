using System;
using ChatterBox.Core.Infrastructure.Facts;

namespace ChatterBox.Domain.Aggregates.RoomAggregate.Facts
{
    [Serializable]
    public class RoomTopicChangedFact : FactAbout<Room>
    {
        public RoomTopicChangedFact(
            Guid aggregateRootId, 
            string topic) 
            : base(aggregateRootId)
        {
            Topic = topic;
        }

        public string Topic { get; protected set; }
    }
}