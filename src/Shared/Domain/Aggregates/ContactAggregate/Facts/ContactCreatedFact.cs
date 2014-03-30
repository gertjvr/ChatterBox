using System;
using ChatterBox.Core.Infrastructure.Facts;

namespace Domain.Aggregates.ContactAggregate.Facts
{
    public class ContactCreatedFact : FactAbout<Contact>
    {
        protected ContactCreatedFact()
        {
            
        }
        
        public ContactCreatedFact(Guid aggregateRootId, string username)
            : base(aggregateRootId)
        {   
            Username = username;
        }

        public string Username { get; protected set; }
    }
}