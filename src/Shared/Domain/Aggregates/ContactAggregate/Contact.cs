using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ChatterBox.Core.Infrastructure.Entities;
using Domain.Aggregates.ContactAggregate.Facts;

namespace Domain.Aggregates.ContactAggregate
{
    [Serializable]
    public class Contact : AggregateRoot
    {
        protected Contact()
        {
            Messages = new Collection<Message>();
        }

        public Contact(string username)
            : this()
        {
            var fact = new ContactCreatedFact(Guid.NewGuid(), username);

            Append(fact);
            Apply(fact);
        }

        public void Apply(ContactCreatedFact fact)
        {
            Id = fact.AggregateRootId;
            Username = fact.Username;
        }

        public void Apply(MessageSendFact fact)
        {
            var message = new Message(fact.ReceiverId, fact.Message);
            Messages.Add(message);
        }

        public string Username { get; private set; }

        public ICollection<Message> Messages { get; set; }

        public void SendMessage(Guid receiverId, string message)
        {
            var fact = new MessageSendFact(Id, receiverId, message);

            Append(fact);
            Apply(fact);
        }
    }
}