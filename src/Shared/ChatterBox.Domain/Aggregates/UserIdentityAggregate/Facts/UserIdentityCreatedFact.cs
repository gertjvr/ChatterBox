using System;
using ChatterBox.Core.Infrastructure.Facts;
using ChatterBox.Domain.Properties;

namespace ChatterBox.Domain.Aggregates.UserIdentityAggregate.Facts
{
    public class UserIdentityCreatedFact : FactAbout<UserIdentity>
    {
        public UserIdentityCreatedFact(
            Guid aggregateRootId, 
            Guid userId, 
            string email, 
            string identity, 
            string providerName) 
            : base(aggregateRootId)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException(LanguageResources.GuidCannotBeEmpty, "userId");

            if (email == null) 
                throw new ArgumentNullException("email");

            if (identity == null) 
                throw new ArgumentNullException("identity");

            if (providerName == null) 
                throw new ArgumentNullException("providerName");

            UserId = userId;
            Email = email;
            Identity = identity;
            ProviderName = providerName;
        }

        public Guid UserId { get; private set; }
        
        public string Email { get; private set; }
        
        public string Identity { get; private set; }

        public string ProviderName { get; private set; }
    }
}