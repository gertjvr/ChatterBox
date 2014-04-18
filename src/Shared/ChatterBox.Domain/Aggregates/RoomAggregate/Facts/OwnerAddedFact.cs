using System;
using ChatterBox.Core.Infrastructure.Facts;
using ChatterBox.Domain.Properties;

namespace ChatterBox.Domain.Aggregates.RoomAggregate.Facts
{
    public class OwnerAddedFact : FactAbout<Room>
    {
        public OwnerAddedFact(
            Guid aggregateRootId, 
            Guid ownerId)
            : base(aggregateRootId)
        {
            if (ownerId == Guid.Empty)
                throw new ArgumentException(LanguageResources.GuidCannotBeEmpty, "ownerId");

            OwnerId = ownerId;
        }

        public Guid OwnerId { get; private set; }
    }
}