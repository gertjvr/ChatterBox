using System;
using ChatterBox.Core.Infrastructure.Facts;

namespace ChatterBox.Domain.Aggregates.UserIdentityAggregate.Facts
{
    [Serializable]
    public class UserIdentityCreatedFact : FactAbout<UserIdentity>
    {
        public Guid UserId { get; set; }
        
        public string Email { get; set; }
        
        public string Identity { get; set; }

        public string ProviderName { get; set; }
    }
}