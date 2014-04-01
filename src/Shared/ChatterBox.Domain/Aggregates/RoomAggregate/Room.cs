using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ChatterBox.Core.Infrastructure.Entities;
using Domain.Aggregates.MessageAggregate;
using Domain.Aggregates.RoomAggregate.Facts;

namespace Domain.Aggregates.RoomAggregate
{
    [Serializable]
    public class Room : AggregateRoot
    {
        protected Room()
        {
            Owners = new Collection<Guid>();
            Messages = new Collection<Message>();
            Contacts = new Collection<Guid>();
        }

        public static Room Create(string topic, Guid ownerId, params Guid[] contacts)
        {
            var fact = new RoomCreatedFact
            {
                AggregateRootId = Guid.NewGuid(),
                Topic = topic,
                OwnerId = ownerId,
                Users = contacts,
            };

            var conversation = new Room();
            conversation.Append(fact);
            conversation.Apply(fact);
            return conversation;
        }

        public void Apply(RoomCreatedFact fact)
        {
            Id = fact.AggregateRootId;
            Topic = fact.Topic;
            Owners.Add(fact.OwnerId);
            Contacts.Add(fact.OwnerId);

            foreach (var c in fact.Users)
                Contacts.Add(c);
        }

        public static string Topic { get; protected set; }

        public ICollection<Message> Messages { get; protected set; }

        public ICollection<Guid> Owners { get; protected set; }

        public ICollection<Guid> Contacts { get; protected set; }

        public void Apply(UserAddedFact fact)
        {
            Contacts.Add(fact.UserId);
        }

        public void Apply(RoomTopicChangedFact fact)
        {
            Topic = fact.Topic;
        }

        public void ChangeTopic(string topic)
        {
            var fact = new RoomTopicChangedFact
            {
                AggregateRootId = Id,
                Topic = topic,
            };

            Append(fact);
            Apply(fact);
        }

        public void AddUser(Guid userId)
        {
            var fact = new UserAddedFact
            {
                AggregateRootId = Id,
                UserId = userId
            };

            Append(fact);
            Apply(fact);
        }
    }
}