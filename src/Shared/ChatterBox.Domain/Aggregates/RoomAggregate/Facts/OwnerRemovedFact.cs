using System;
using ChatterBox.Core.Infrastructure.Facts;

namespace ChatterBox.Domain.Aggregates.RoomAggregate.Facts
{
    public class OwnerRemovedFact : FactAbout<Room>
    {
        public OwnerRemovedFact(
            Guid aggregateRootId, 
            Guid ownerId)
            : base(aggregateRootId)
        {
            OwnerId = ownerId;
        }

        public Guid OwnerId { get; protected set; }
    }
}