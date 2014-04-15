using System;
using ChatterBox.Core.Infrastructure.Facts;

namespace ChatterBox.Domain.Aggregates.UserAggregate.Facts
{
    public class ConnectedClientRemovedFact : FactAbout<User>
    {
        public ConnectedClientRemovedFact(
            Guid aggregateRootId, 
            Guid clientId) 
            : base(aggregateRootId)
        {
            ClientId = clientId;
        }

        public Guid ClientId { get; protected set; }
    }
}