using System;
using ChatterBox.Core.Infrastructure.Facts;

namespace ChatterBox.Domain.Aggregates.UserAggregate.Facts
{
    public class ConnectedClientRegisteredFact : FactAbout<User>
    {
        public ConnectedClientRegisteredFact(Guid aggregateRootId, Guid clientId) 
            : base(aggregateRootId)
        {
            ClientId = clientId;
        }

        public Guid ClientId { get; private set; }
    }
}