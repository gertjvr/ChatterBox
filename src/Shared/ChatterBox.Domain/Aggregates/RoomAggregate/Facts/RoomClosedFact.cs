using System;
using ChatterBox.Core.Infrastructure.Facts;

namespace ChatterBox.Domain.Aggregates.RoomAggregate.Facts
{
    public class RoomClosedFact : FactAbout<Room>
    {
        public RoomClosedFact(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; protected set; }
    }
}