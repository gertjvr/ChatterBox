using System;
using System.Collections.Generic;
using ChatterBox.Core.Infrastructure.Entities;
using Domain.Aggregates.ContactAggregate.Facts;

namespace Domain.Aggregates.ContactAggregate
{
    [Serializable]
    public class Contact : AggregateRoot
    {
        protected Contact()
        {   
        }

        public static Contact Create(string username)
        {
            var fact = new ContactCreatedFact
            {
                AggregateRootId = Guid.NewGuid(),
                Username = username
            };

            var contact = new Contact();
            contact.Append(fact);
            contact.Apply(fact);
            return contact;
        }

        public void Apply(ContactCreatedFact fact)
        {
            Id = fact.AggregateRootId;
            Username = fact.Username;
        }

        public string Username { get; protected set; }
    }
}