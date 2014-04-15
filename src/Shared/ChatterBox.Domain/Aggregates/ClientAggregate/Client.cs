using System;
using ChatterBox.Core.Infrastructure.Entities;
using ChatterBox.Domain.Aggregates.ClientAggregate.Facts;
using ChatterBox.Domain.Aggregates.UserAggregate.Facts;

namespace ChatterBox.Domain.Aggregates.ClientAggregate
{
    [Serializable]
    public class Client : AggregateRoot
    {
        public Client(Guid clientId, Guid userId, string userAgent, DateTimeOffset lastActivity)
        {
            var fact = new ClientCreatedFact(
                clientId,
                userId,
                userAgent,
                lastActivity);

            Append(fact);
            Apply(fact);
        }

        public void Apply(ClientCreatedFact fact)
        {
            Id = fact.AggregateRootId;
            UserId = fact.UserId;
            UserAgent = fact.UserAgent;
            LastActivity = fact.LastActivity;
        }

        public Guid UserId { get; protected set; }
        
        public string UserAgent { get; protected set; }

        public DateTimeOffset LastActivity { get; protected set; }

        public void UpdateLastActivity(DateTimeOffset lastActivity)
        {
            var fact = new ClientLastActivityUpdatedFact(
                Id,
                lastActivity);

            Append(fact);
            Apply(fact);
        }

        public void Apply(ClientLastActivityUpdatedFact fact)
        {
            LastActivity = fact.LastActivity;
        }

        public void UpdateUserAgent(string userAgent)
        {
            var fact = new ClientUserAgentUpdatedFact(Id, userAgent);

            Append(fact);
            Apply(fact);
        }

        public void Apply(ClientUserAgentUpdatedFact fact)
        {
            UserAgent = fact.UserAgent;
        }
    }
}