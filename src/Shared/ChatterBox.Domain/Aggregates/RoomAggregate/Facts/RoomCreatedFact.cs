using System;
using ChatterBox.Core.Infrastructure.Facts;

namespace ChatterBox.Domain.Aggregates.RoomAggregate.Facts
{
    [Serializable]
    public class RoomCreatedFact : FactAbout<Room>
    {
        public RoomCreatedFact(
            Guid aggregateRootId,
            string name,
            Guid ownerId)
            : base(aggregateRootId)
        {
            Name = name;
            OwnerId = ownerId;
        }

        public string Name { get; set; }

        public Guid OwnerId { get; set; }
    }
}