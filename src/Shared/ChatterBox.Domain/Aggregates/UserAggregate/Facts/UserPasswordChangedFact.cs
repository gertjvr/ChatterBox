using System;
using ChatterBox.Core.Infrastructure.Facts;

namespace ChatterBox.Domain.Aggregates.UserAggregate.Facts
{
    public class UserPasswordChangedFact : FactAbout<User>
    {
        public UserPasswordChangedFact(
            Guid aggregateRootId, 
            string newHashedPassword) 
            : base(aggregateRootId)
        {
            NewHashedPassword = newHashedPassword;
        }

        public string NewHashedPassword { get; set; }
    }
}