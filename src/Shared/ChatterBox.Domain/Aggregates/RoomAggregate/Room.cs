using System;
using System.Collections.Generic;
using ChatterBox.Core.Infrastructure.Entities;
using ChatterBox.Domain.Aggregates.RoomAggregate.Facts;

namespace ChatterBox.Domain.Aggregates.RoomAggregate
{
    [Serializable]
    public class Room : AggregateRoot
    {
        private List<Guid> _owners;
        private List<Guid> _contacts;
        private List<Guid> _allowed;

        protected Room()
        {
        }

        public Room(string name, Guid ownerId, bool privateRoom = false)
        {
            var fact = new RoomCreatedFact(
                Guid.NewGuid(),
                name,
                privateRoom,
                ownerId);

            Append(fact);
            Apply(fact);
        }

        public string Name { get; protected set; }

        public string Topic { get; protected set; }

        public bool Private { get; protected set; }
        
        public bool Closed { get; protected set; }
        
        public string Welcome { get; protected set; }

        public Guid CreatorId { get; protected set; }

        public IEnumerable<Guid> Owners { get { return _owners; } }

        public IEnumerable<Guid> Contacts { get { return _contacts; } }

        public IEnumerable<Guid> Allowed { get { return _allowed; } } 

        public void Apply(RoomCreatedFact fact)
        {
            Id = fact.AggregateRootId;
            Name = fact.Name;
            CreatorId = fact.CreatorId;

            _owners = new List<Guid> { fact.CreatorId };
            _contacts = new List<Guid> { fact.CreatorId };
            _allowed = new List<Guid> { fact.CreatorId };
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
            _contacts.Add(fact.UserId);
        }

        public void Leave(Guid userId)
        {
            var fact = new UserLeftFact(userId);

            Append(fact);
            Apply(fact);
        }

        public void Apply(UserLeftFact fact)
        {
            _contacts.Remove(fact.UserId);
        }

        public void AddOwner(Guid userId)
        {
            var fact = new OwnerAddedFact(userId);

            Append(fact);
            Apply(fact);
        }

        public void Apply(OwnerAddedFact fact)
        {
            _owners.Add(fact.OwnerId);
        }

        public void Close(Guid userId)
        {
            var fact = new RoomClosedFact(userId);

            Append(fact);
            Apply(fact);
        }

        public void Apply(RoomClosedFact fact)
        {
            Closed = true;
        }

        public void Open(Guid userId)
        {
            var fact = new RoomOpenedFact(userId);

            Append(fact);
            Apply(fact);
        }

        public void Apply(RoomOpenedFact fact)
        {
            Closed = false;
        }

        public void AllowUser(Guid userId)
        {
            var fact = new UserAllowedFact(userId);

            Append(fact);
            Apply(fact);
        }

        public void Apply(UserAllowedFact fact)
        {
            _allowed.Add(fact.UserId);
        }

        public void RemoveOwner(Guid ownerId)
        {
            var fact = new OwnerRemovedFact(ownerId);

            Append(fact);
            Apply(fact);
        }

        public void Apply(OwnerRemovedFact fact)
        {
            _owners.Remove(fact.OwnerId);
        }
    }
}