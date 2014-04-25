using System;
using ChatterBox.Core.Infrastructure.Facts;
using ChatterBox.Domain.Properties;

namespace ChatterBox.Domain.Aggregates.UserAggregate.Facts
{
    public class ConnectedClientRegisteredFact : FactAbout<User>
    {
        public ConnectedClientRegisteredFact(Guid aggregateRootId, Guid clientId) 
            : base(aggregateRootId)
        {
            if (clientId == Guid.Empty)
                throw new ArgumentException(LanguageResources.GuidCannotBeEmpty, "clientId");

            ClientId = clientId;
        }

        public Guid ClientId { get; private set; }
    }
}