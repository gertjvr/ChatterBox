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
        }

        public Room(string name, Guid ownerId)
        {
            var fact = new RoomCreatedFact
            {
                AggregateRootId = Guid.NewGuid(),
                Name = name,
                OwnerId = ownerId,
            };

            Append(fact);
            Apply(fact);
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
            Name = fact.Name;

            Owners = new Collection<Guid> { fact.OwnerId };
            Contacts = new Collection<Guid> { fact.OwnerId };
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