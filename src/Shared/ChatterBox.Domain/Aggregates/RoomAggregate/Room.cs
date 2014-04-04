using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ChatterBox.Core.Infrastructure.Entities;
using ChatterBox.Domain.Aggregates.MessageAggregate;
using ChatterBox.Domain.Aggregates.RoomAggregate.Facts;

namespace ChatterBox.Domain.Aggregates.RoomAggregate
{
    [Serializable]
    public class Room : AggregateRoot
    {
        protected Room()
        {
            Owners = new Collection<Guid>();
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

        public string Name { get; protected set; }

        public string Topic { get; protected set; }

        public bool Private { get; protected set; }
        
        public bool Closed { get; protected set; }
        
        public string Welcome { get; protected set; }

        public ICollection<Guid> Owners { get; protected set; }

        public ICollection<Guid> Contacts { get; protected set; }

        public void Apply(RoomCreatedFact fact)
        {
            Id = fact.AggregateRootId;
            Topic = fact.Topic;
            Owners.Add(fact.OwnerId);
            Contacts.Add(fact.OwnerId);

            foreach (var c in fact.Users)
                Contacts.Add(c);
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

        public void Apply(RoomTopicChangedFact fact)
        {
            Topic = fact.Topic;
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

        public void Apply(UserAddedFact fact)
        {
            Contacts.Add(fact.UserId);
        }
    }
}