using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ChatterBox.Core.Infrastructure.Entities;
using Domain.Aggregates.ConversationAggregate.Facts;

namespace Domain.Aggregates.ConversationAggregate
{
    [Serializable]
    public class Conversation : AggregateRoot
    {
        protected Conversation()
        {
            Owners = new Collection<Guid>();
            Messages = new Collection<Message>();
            Contacts = new Collection<Guid>();
        }

        public static Conversation Create(string topic, Guid ownerId, params Guid[] contacts)
        {
            var fact = new ConversationCreatedFact
            {
                AggregateRootId = Guid.NewGuid(),
                Topic = topic,
                OwnerId = ownerId,
                Contacts = contacts,
            };

            var conversation = new Conversation();
            conversation.Append(fact);
            conversation.Apply(fact);
            return conversation;
        }

        public void Apply(ConversationCreatedFact fact)
        {
            Id = fact.AggregateRootId;
            Topic = fact.Topic;
            Owners.Add(fact.OwnerId);
            Contacts.Add(fact.OwnerId);

            foreach (var c in fact.Contacts)
                Contacts.Add(c);
        }

        public static string Topic { get; protected set; }

        public ICollection<Message> Messages { get; protected set; }

        public ICollection<Guid> Owners { get; protected set; }

        public ICollection<Guid> Contacts { get; protected set; }

        public void Apply(ContactAddedFact fact)
        {
            Contacts.Add(fact.ContactId);
        }

        public void Apply(MessageReceivedFact fact)
        {
            Messages.Add(new Message(fact.ContactId, fact.Content, fact.CreatedAt));
        }

        public void Apply(TopicChangedFact fact)
        {
            Topic = fact.Topic;
        }

        public void ChangeTopic(string topic)
        {
            var fact = new TopicChangedFact
            {
                AggregateRootId = Id,
                Topic = topic,
            };

            Append(fact);
            Apply(fact);
        }

        public void AddMessage(Guid contactId, string content, DateTimeOffset createdAt)
        {
            var fact = new MessageReceivedFact
            {
                AggregateRootId = Id,
                ContactId = contactId,
                Content = content,
                CreatedAt = createdAt,
            };

            Append(fact);
            Apply(fact);
        }

        public void AddContact(Guid contactId)
        {
            var fact = new ContactAddedFact
            {
                AggregateRootId = Id,
                ContactId = contactId
            };

            Append(fact);
            Apply(fact);
        }
    }
}