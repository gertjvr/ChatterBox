using System;
using ChatterBox.Core.Infrastructure.Facts;

namespace ChatterBox.Domain.Aggregates.UserAggregate.Facts
{
    public class UserNameUpdatedFact : FactAbout<User>
    {
        public UserNameUpdatedFact(
            Guid aggregateRootId, 
            string newUserName) 
            : base(aggregateRootId)
        {
            NewUserName = newUserName;
        }

        public string NewUserName { get; protected set; }
    }
}