using System;
using ChatterBox.Core.Infrastructure.Facts;

namespace ChatterBox.Domain.Aggregates.RoomAggregate.Facts
{
    [Serializable]
    public class RoomCreatedFact : FactAbout<Room>
    {
        public string Name { get; set; }
        
        public Guid OwnerId { get; set; }
    }
}