using System;
using ChatterBox.Core.Infrastructure.Entities;
using ChatterBox.Domain.Aggregates.UserIdentityAggregate.Facts;
using ChatterBox.Domain.Properties;

namespace ChatterBox.Domain.Aggregates.UserIdentityAggregate
{
    public class UserIdentity : AggregateRoot
    {
        protected UserIdentity()
        {   
        }

        public UserIdentity(Guid userId, string email, string identity, string providerName)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException(LanguageResources.GuidCannotBeEmpty, "userId");

            if (email == null) 
                throw new ArgumentNullException("email");

            if (identity == null) 
                throw new ArgumentNullException("identity");

            if (providerName == null) 
                throw new ArgumentNullException("providerName");

            var fact = new UserIdentityCreatedFact(
                Guid.NewGuid(),
                userId,
                email,
                identity,
                providerName);

            Append(fact);
            Apply(fact);
        }

        public Guid UserId { get; private set; }

        public string Email { get; private set; }

        public string Identity { get; private set; }

        public string ProviderName { get; private set; }

        public void Apply(UserIdentityCreatedFact fact)
        {
            if (fact == null) 
                throw new ArgumentNullException("fact");

            Id = fact.AggregateRootId;
            UserId = fact.UserId;
            Email = fact.Email;
            Identity = fact.Identity;
            ProviderName = fact.ProviderName;
        }
    }
}