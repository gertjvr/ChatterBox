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
            if (newUserName == null) 
                throw new ArgumentNullException("newUserName");

            NewUserName = newUserName;
        }

        public string NewUserName { get; private set; }
    }
}