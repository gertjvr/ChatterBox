using System;
using ChatterBox.Core.Infrastructure.Facts;
using ChatterBox.Domain.Properties;

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
            if (userId == Guid.Empty)
                throw new ArgumentException(LanguageResources.GuidCannotBeEmpty, "userId");
            
            if (userAgent == null) 
                throw new ArgumentNullException("userAgent");

            UserId = userId;
            UserAgent = userAgent;
            LastActivity = lastActivity;
        }

        public Guid UserId { get; private set; }

        public string UserAgent { get; private set; }

        public DateTimeOffset LastActivity { get; private set; }
    }
}