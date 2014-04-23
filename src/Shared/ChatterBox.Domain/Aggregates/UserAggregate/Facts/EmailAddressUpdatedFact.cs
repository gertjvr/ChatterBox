using System;
using ChatterBox.Core.Infrastructure.Facts;

namespace ChatterBox.Domain.Aggregates.UserAggregate.Facts
{
    public class EmailAddressUpdatedFact: FactAbout<User>
    {
        public EmailAddressUpdatedFact(
            Guid aggregateRootId, 
            string newEmailAddress) 
            : base(aggregateRootId)
        {
            if (newEmailAddress == null)
                throw new ArgumentNullException("newEmailAddress");

            NewEmailAddress = newEmailAddress;
        }

        public string NewEmailAddress { get; private set; }
    }
}