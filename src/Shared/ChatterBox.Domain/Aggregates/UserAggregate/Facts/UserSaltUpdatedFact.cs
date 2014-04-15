using System;
using ChatterBox.Core.Infrastructure.Facts;

namespace ChatterBox.Domain.Aggregates.UserAggregate.Facts
{
    public class UserSaltUpdatedFact : FactAbout<User>
    {
        public UserSaltUpdatedFact(
            Guid aggregateRootId, 
            string newSalt) 
            : base(aggregateRootId)
        {
            NewSalt = newSalt;
        }

        public string NewSalt { get; protected set; }
    }
}
