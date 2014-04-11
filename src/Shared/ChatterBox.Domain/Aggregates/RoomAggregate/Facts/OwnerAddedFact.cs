using System;
using ChatterBox.Core.Infrastructure.Facts;

namespace ChatterBox.Domain.Aggregates.RoomAggregate.Facts
{
    public class OwnerAddedFact : FactAbout<Room>
    {
        public OwnerAddedFact(Guid ownerId)
        {
            OwnerId = ownerId;
        }

        public Guid OwnerId { get; protected set; }
    }
}