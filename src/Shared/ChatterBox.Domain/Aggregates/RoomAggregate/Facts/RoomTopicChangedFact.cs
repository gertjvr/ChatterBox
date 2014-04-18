using System;
using ChatterBox.Core.Infrastructure.Facts;

namespace ChatterBox.Domain.Aggregates.RoomAggregate.Facts
{
    public class RoomTopicChangedFact : FactAbout<Room>
    {
        public RoomTopicChangedFact(
            Guid aggregateRootId, 
            string newTopic) 
            : base(aggregateRootId)
        {
            if (newTopic == null) 
                throw new ArgumentNullException("newTopic");

            NewTopic = newTopic;
        }

        public string NewTopic { get; private set; }
    }
}