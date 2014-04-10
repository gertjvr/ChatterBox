using System;
using ChatterBox.Core.Infrastructure.Facts;

namespace ChatterBox.Domain.Aggregates.UserAggregate.Facts
{
    public class UserStatusChangedFact : FactAbout<User>
    {
        public UserStatusChangedFact(
            Guid aggregateRootId, 
            UserStatus status) 
            : base(aggregateRootId)
        {
            Status = status;
        }

        public UserStatus Status { get; protected set; }
    }
}
