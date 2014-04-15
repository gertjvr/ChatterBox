using System;
using System.Collections.Generic;
using ChatterBox.Core.Infrastructure.Entities;
using ChatterBox.Domain.Aggregates.RoomAggregate.Facts;

namespace ChatterBox.Domain.Aggregates.RoomAggregate
{
    [Serializable]
    public class Room : AggregateRoot
    {
        private readonly List<Guid> _owners = new List<Guid>();
        private readonly List<Guid> _users = new List<Guid>();
        private readonly List<Guid> _allowedUsers = new List<Guid>();

        protected Room()
        {
        }

        public Room(string name, Guid creatorId, bool privateRoom = false)
        {
            var fact = new RoomCreatedFact(
                Guid.NewGuid(),
                name,
                privateRoom,
                creatorId);

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

        public IEnumerable<Guid> Users { get { return _users; } }

        public IEnumerable<Guid> AllowedUsers { get { return _allowedUsers; } }
        
        public string InviteCode { get; protected set; }

        public void Apply(RoomCreatedFact fact)
        {
            Id = fact.AggregateRootId;
            Name = fact.Name;
            CreatorId = fact.CreatorId;

            _owners.Add(fact.CreatorId);
            _allowedUsers.Add(fact.CreatorId);
        }

        public void ChangeTopic(string topic)
        {
            var fact = new RoomTopicChangedFact(
                Id,
                topic);

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
            _users.Add(fact.UserId);
        }

        public void Leave(Guid userId)
        {
            var fact = new UserLeftFact(
                Id, 
                userId);

            Append(fact);
            Apply(fact);
        }

        public void Apply(UserLeftFact fact)
        {
            _users.Remove(fact.UserId);
        }

        public void AddOwner(Guid ownerId)
        {
            var fact = new OwnerAddedFact(
                Id, 
                ownerId);

            Append(fact);
            Apply(fact);
        }

        public void Apply(OwnerAddedFact fact)
        {
            _owners.Add(fact.OwnerId);
        }

        public void Close()
        {
            var fact = new RoomClosedFact(Id);

            Append(fact);
            Apply(fact);
        }

        public void Apply(RoomClosedFact fact)
        {
            Closed = true;
        }

        public void Open()
        {
            var fact = new RoomOpenedFact(Id);

            Append(fact);
            Apply(fact);
        }

        public void Apply(RoomOpenedFact fact)
        {
            Closed = false;
        }

        public void AllowUser(Guid userId)
        {
            var fact = new UserAllowedFact(Id, userId);

            Append(fact);
            Apply(fact);
        }

        public void Apply(UserAllowedFact fact)
        {
            _allowedUsers.Add(fact.UserId);
        }

        public void RemoveOwner(Guid ownerId)
        {
            var fact = new OwnerRemovedFact(Id, ownerId);

            Append(fact);
            Apply(fact);
        }

        public void Apply(OwnerRemovedFact fact)
        {
            _owners.Remove(fact.OwnerId);
        }

        public void UnallowUser(Guid userId)
        {
            var fact = new UserUnallowedFact(Id, userId);

            Append(fact);
            Apply(fact);
        }

        public void Apply(UserUnallowedFact fact)
        {
            _allowedUsers.Remove(fact.UserId);
        }

        public void SetInviteCode(string inviteCode)
        {
            var fact = new InviteCodeSetFact(Id, inviteCode);

            Append(fact);
            Apply(fact);
        }

        public void Apply(InviteCodeSetFact fact)
        {
            InviteCode = fact.InviteCode;
        }
    }
}