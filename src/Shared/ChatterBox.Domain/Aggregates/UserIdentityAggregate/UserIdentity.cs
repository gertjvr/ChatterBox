using System;
using ChatterBox.Core.Infrastructure.Entities;
using ChatterBox.Domain.Aggregates.UserIdentityAggregate.Facts;

namespace ChatterBox.Domain.Aggregates.UserIdentityAggregate
{
    [Serializable]
    public class UserIdentity : AggregateRoot
    {
        protected UserIdentity()
        {   
        }

        public static UserIdentity Create(Guid userId, string email, string identity, string providerName)
        {
            var fact = new UserIdentityCreatedFact
            {
                AggregateRootId = Guid.NewGuid(),
                UserId = userId,
                Email = email,
                Identity = identity,
                ProviderName = providerName,
            };

            var userIdentity = new UserIdentity();
            userIdentity.Append(fact);
            userIdentity.Apply(fact);
            return userIdentity;
        }

        public Guid UserId { get; protected set; }

        public string Email { get; protected set; }

        public string Identity { get; protected set; }

        public string ProviderName { get; protected set; }

        public void Apply(UserIdentityCreatedFact fact)
        {
            Id = fact.AggregateRootId;
            UserId = fact.UserId;
            Email = fact.Email;
            Identity = fact.Identity;
            ProviderName = fact.ProviderName;
        }
    }
}