using System;
using ChatterBox.Core.Infrastructure.Facts;

namespace ChatterBox.Domain.Aggregates.RoomAggregate.Facts
{
    public class UserLeftFact : FactAbout<Room>
    {
        public UserLeftFact(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; protected set; }
    }
}