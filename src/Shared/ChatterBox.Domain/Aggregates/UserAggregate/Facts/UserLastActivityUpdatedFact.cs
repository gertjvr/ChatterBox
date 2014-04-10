using System;
using ChatterBox.Core.Infrastructure.Facts;

namespace ChatterBox.Domain.Aggregates.UserAggregate.Facts
{
    public class UserLastActivityUpdatedFact : FactAbout<User>
    {
        public UserLastActivityUpdatedFact(
            Guid aggregateRootId, 
            DateTimeOffset lastActivity) 
            : base(aggregateRootId)
        {
            LastActivity = lastActivity;
        }

        public DateTimeOffset LastActivity { get; protected set; }
    }
}