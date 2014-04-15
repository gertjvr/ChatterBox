using System;
using ChatterBox.Core.Infrastructure.Facts;

namespace ChatterBox.Domain.Aggregates.UserAggregate.Facts
{
    public class UserPasswordUpdatedFact : FactAbout<User>
    {
        public UserPasswordUpdatedFact(
            Guid aggregateRootId, 
            string newHashedPassword) 
            : base(aggregateRootId)
        {
            NewHashedPassword = newHashedPassword;
        }

        public string NewHashedPassword { get; set; }
    }
}