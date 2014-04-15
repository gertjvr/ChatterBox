using System;
using ChatterBox.Core.Infrastructure.Facts;

namespace ChatterBox.Domain.Aggregates.ClientAggregate.Facts
{
    public class ClientUserAgentUpdatedFact : FactAbout<Client>
    {
        public ClientUserAgentUpdatedFact(Guid aggregateRootId, string userAgent)
            : base(aggregateRootId)
        {
            UserAgent = userAgent;
        }

        public string UserAgent { get; protected set; }
    }
}