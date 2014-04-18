using System;
using ChatterBox.Core.Infrastructure.Entities;
using ChatterBox.Domain.Aggregates.ClientAggregate.Facts;
using ChatterBox.Domain.Properties;

namespace ChatterBox.Domain.Aggregates.ClientAggregate
{
    public class Client : AggregateRoot
    {
        protected Client()
        {   
        }

        public Client(Guid id, Guid userId, string userAgent, DateTimeOffset lastActivity)
        {
            if (id == Guid.Empty) 
                throw new ArgumentException(LanguageResources.GuidCannotBeEmpty, "id");

            if (userId == Guid.Empty)
                throw new ArgumentException(LanguageResources.GuidCannotBeEmpty, "userId");

            if (userAgent == null) 
                throw new ArgumentNullException("userAgent");

            var fact = new ClientCreatedFact(
                id,
                userId,
                userAgent,
                lastActivity);

            Append(fact);
            Apply(fact);
        }

        public Guid UserId { get; private set; }

        public string UserAgent { get; private set; }

        public DateTimeOffset LastActivity { get; private set; }

        public void Apply(ClientCreatedFact fact)
        {
            if (fact == null)
                throw new ArgumentNullException("fact");

            Id = fact.AggregateRootId;
            UserId = fact.UserId;
            UserAgent = fact.UserAgent;
            LastActivity = fact.LastActivity;
        }

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
            if (fact == null)
                throw new ArgumentNullException("fact");

            LastActivity = fact.LastActivity;
        }

        public void UpdateUserAgent(string userAgent)
        {
            if (userAgent == null)
                throw new ArgumentNullException("userAgent");

            var fact = new ClientUserAgentUpdatedFact(Id, userAgent);

            Append(fact);
            Apply(fact);
        }

        public void Apply(ClientUserAgentUpdatedFact fact)
        {
            if (fact == null)
                throw new ArgumentNullException("fact");

            UserAgent = fact.UserAgent;
        }
    }
}