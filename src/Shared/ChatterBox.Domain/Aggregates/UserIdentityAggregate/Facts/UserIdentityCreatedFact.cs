using System;
using ChatterBox.Core.Infrastructure.Facts;

namespace ChatterBox.Domain.Aggregates.UserIdentityAggregate.Facts
{
    [Serializable]
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
            UserId = userId;
            Email = email;
            Identity = identity;
            ProviderName = providerName;
        }

        public Guid UserId { get; protected set; }
        
        public string Email { get; protected set; }
        
        public string Identity { get; protected set; }

        public string ProviderName { get; protected set; }
    }
}