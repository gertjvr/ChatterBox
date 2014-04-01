using System;
using ChatterBox.Core.Infrastructure.Facts;

namespace Domain.Aggregates.RoomAggregate.Facts
{
    [Serializable]
    public class UserAddedFact : FactAbout<Room>
    {
        public Guid UserId { get; set; }
    }
}