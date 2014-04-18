using System;
using ChatterBox.Core.Infrastructure.Facts;
using ChatterBox.Domain.Properties;

namespace ChatterBox.Domain.Aggregates.RoomAggregate.Facts
{
    public class RoomCreatedFact : FactAbout<Room>
    {
        public RoomCreatedFact(
            Guid aggregateRootId, 
            string name, 
            Guid creatorId, 
            bool privateRoom)
            : base(aggregateRootId)
        {
            if (name == null) throw new ArgumentNullException("name");

            if (creatorId == Guid.Empty)
                throw new ArgumentException(LanguageResources.GuidCannotBeEmpty, "creatorId");

            Name = name;
            CreatorId = creatorId;
            PrivateRoom = privateRoom;
        }

        public string Name { get; private set; }

        public Guid CreatorId { get; private set; }

        public bool PrivateRoom { get; private set; }
    }
}