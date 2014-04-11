using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ChatterBox.Core.Infrastructure.Entities;
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
            var fact = new RoomCreatedFact(
                Guid.NewGuid(),
                name,
                ownerId);

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

        public void ChangeTopic(string topic, Guid userId, DateTimeOffset changedAt)
        {
            var fact = new RoomTopicChangedFact(
                Id,
                topic,
                userId,
                changedAt);

            Append(fact);
            Apply(fact);
        }

        public void Apply(RoomTopicChangedFact fact)
        {
            Topic = fact.NewTopic;
        }

        public void Join(Guid userId)
        {
            var fact = new UserJoinedFact(
                Id,
                userId);

            Append(fact);
            Apply(fact);
        }

        public void Apply(UserJoinedFact fact)
        {
            Contacts.Add(fact.UserId);
        }

        public void Leave(Guid userId)
        {
            var fact = new UserLeftFact(userId);

            Append(fact);
            Apply(fact);
        }

        public void Apply(UserLeftFact fact)
        {
            Contacts.Remove(fact.UserId);
        }

        public void AddOwner(Guid userId)
        {
            var fact = new OwnerAddedFact(userId);

            Append(fact);
            Apply(fact);
        }

        public void Apply(OwnerAddedFact fact)
        {
            Owners.Add(fact.OwnerId);
        }
    }
}