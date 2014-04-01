using System;
using System.Collections.Generic;
using ChatterBox.Core.Infrastructure.Facts;

namespace Domain.Aggregates.RoomAggregate.Facts
{
    [Serializable]
    public class RoomCreatedFact : FactAbout<Room>
    {
        public string Topic { get; set; }
        
        public Guid OwnerId { get; set; }
        
        public IEnumerable<Guid> Users { get; set; }
    }
}