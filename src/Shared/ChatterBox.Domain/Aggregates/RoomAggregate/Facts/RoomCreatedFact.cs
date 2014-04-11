using System;
using ChatterBox.Core.Infrastructure.Facts;

namespace ChatterBox.Domain.Aggregates.RoomAggregate.Facts
{
    public class RoomCreatedFact : FactAbout<Room>
    {
        public RoomCreatedFact(
            Guid aggregateRootId, 
            string name, 
            bool privateRoom, 
            Guid ownerId)
            : base(aggregateRootId)
        {
            Name = name;
            PrivateRoom = privateRoom;
            OwnerId = ownerId;
        }

        public string Name { get; protected set; }
        
        public bool PrivateRoom { get; protected set; }

        public Guid OwnerId { get; protected set; }
    }
}