using System;
using ChatterBox.Core.Infrastructure.Facts;

namespace ChatterBox.Domain.Aggregates.RoomAggregate.Facts
{
    [Serializable]
    public class UserJoinedFact : FactAbout<Room>
    {
        public UserJoinedFact(
            Guid aggregateRootId, 
            Guid userId) 
            : base(aggregateRootId)
        {
            UserId = userId;
        }

        public Guid UserId { get; protected set; }
    }
}