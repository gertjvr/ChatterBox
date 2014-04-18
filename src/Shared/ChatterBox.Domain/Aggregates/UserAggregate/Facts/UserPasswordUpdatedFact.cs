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
            if (newHashedPassword == null) 
                throw new ArgumentNullException("newHashedPassword");

            NewHashedPassword = newHashedPassword;
        }

        public string NewHashedPassword { get; private set; }
    }
}