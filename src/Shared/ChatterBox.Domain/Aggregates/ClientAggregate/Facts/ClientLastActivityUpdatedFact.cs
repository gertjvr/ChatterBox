using System;
using ChatterBox.Core.Infrastructure.Facts;
using ChatterBox.Domain.Aggregates.UserAggregate;

namespace ChatterBox.Domain.Aggregates.ClientAggregate.Facts
{
    public class ClientLastActivityUpdatedFact : FactAbout<User>
    {
        public ClientLastActivityUpdatedFact(
            Guid aggregateRootId,
            DateTimeOffset lastActivity)
            : base(aggregateRootId)
        {
            LastActivity = lastActivity;
        }

        public DateTimeOffset LastActivity { get; private set; }
    }
}