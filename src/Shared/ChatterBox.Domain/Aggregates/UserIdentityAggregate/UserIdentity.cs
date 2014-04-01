using System;
using ChatterBox.Core.Infrastructure.Entities;
using Domain.Aggregates.UserIdentityAggregate.Facts;

namespace Domain.Aggregates.UserIdentityAggregate
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

        public void Apply(UserIdentityCreatedFact fact)
        {
            throw new NotImplementedException();
        }
    }
}