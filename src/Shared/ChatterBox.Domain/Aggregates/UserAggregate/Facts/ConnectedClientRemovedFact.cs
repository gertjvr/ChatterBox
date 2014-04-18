using System;
using ChatterBox.Core.Infrastructure.Facts;
using ChatterBox.Domain.Properties;

namespace ChatterBox.Domain.Aggregates.UserAggregate.Facts
{
    public class ConnectedClientRemovedFact : FactAbout<User>
    {
        public ConnectedClientRemovedFact(
            Guid aggregateRootId, 
            Guid clientId) 
            : base(aggregateRootId)
        {
            if (clientId == Guid.Empty)
                throw new ArgumentException(LanguageResources.GuidCannotBeEmpty, "clientId");

            ClientId = clientId;
        }

        public Guid ClientId { get; private set; }
    }
}