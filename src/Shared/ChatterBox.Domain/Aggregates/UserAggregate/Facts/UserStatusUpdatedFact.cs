using System;
using ChatterBox.Core.Infrastructure.Facts;

namespace ChatterBox.Domain.Aggregates.UserAggregate.Facts
{
    public class UserStatusUpdatedFact : FactAbout<User>
    {
        public UserStatusUpdatedFact(
            Guid aggregateRootId, 
            UserStatus status) 
            : base(aggregateRootId)
        {
            Status = status;
        }

        public UserStatus Status { get; protected set; }
    }
}
