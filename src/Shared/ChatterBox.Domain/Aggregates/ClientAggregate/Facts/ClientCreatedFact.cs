using System;
using ChatterBox.Core.Infrastructure.Facts;

namespace ChatterBox.Domain.Aggregates.ClientAggregate.Facts
{
    [Serializable]
    public class ClientCreatedFact : FactAbout<Client>
    {
        public ClientCreatedFact(
            Guid aggregateRootId, 
            Guid userId, 
            string userAgent, 
            DateTimeOffset lastActivity)
            : base(aggregateRootId)
        {
            UserId = userId;
            UserAgent = userAgent;
            LastActivity = lastActivity;
        }

        public Guid UserId { get; protected set; }

        public string UserAgent { get; protected set; }

        public DateTimeOffset LastActivity { get; protected set; }
    }
}