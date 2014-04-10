using System;
using ChatterBox.Core.Infrastructure.Facts;

namespace ChatterBox.Domain.Aggregates.UserAggregate.Facts
{
    public class UserNameChangedFact : FactAbout<User>
    {
        public UserNameChangedFact(
            Guid aggregateRootId, 
            string newUserName) 
            : base(aggregateRootId)
        {
            NewUserName = newUserName;
        }

        public string NewUserName { get; protected set; }
    }
}