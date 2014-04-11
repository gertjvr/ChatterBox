using System;
using ChatterBox.Core.Infrastructure.Facts;

namespace ChatterBox.Domain.Aggregates.RoomAggregate.Facts
{
    public class RoomOpenedFact : FactAbout<Room>
    {
        public RoomOpenedFact(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; protected set; }
    }
}