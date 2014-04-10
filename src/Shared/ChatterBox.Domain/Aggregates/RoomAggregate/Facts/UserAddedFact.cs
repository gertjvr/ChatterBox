using System;
using ChatterBox.Core.Infrastructure.Facts;

namespace ChatterBox.Domain.Aggregates.RoomAggregate.Facts
{
    [Serializable]
    public class UserAddedFact : FactAbout<Room>
    {
        public UserAddedFact(
            Guid aggregateRootId, 
            Guid userId) 
            : base(aggregateRootId)
        {
            UserId = userId;
        }

        public Guid UserId { get; protected set; }
    }
}