﻿using System;
using System.Collections.Generic;
using ChatterBox.Core.Infrastructure.Entities;
using ChatterBox.Domain.Aggregates.RoomAggregate.Facts;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.Domain.Properties;

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

        public Room(string name, Guid creatorId, string welcomeMessage, string topic, bool privateRoom)
            : this(Guid.NewGuid(), name, creatorId, welcomeMessage, topic, privateRoom)
        {   
        }

        public Room(Guid id, string name, Guid creatorId, string welcomeMessage, string topic, bool privateRoom)
        {
            if (id == null)
                throw new ArgumentException(LanguageResources.GuidCannotBeEmpty, "id");

            if (name == null) 
                throw new ArgumentNullException("name");

            if (creatorId == null)
                throw new ArgumentNullException("creatorId");
            
            if (welcomeMessage == null) 
                throw new ArgumentNullException("welcomeMessage");

            if (topic == null) 
                throw new ArgumentNullException("topic");

            var fact = new RoomCreatedFact(
                id,
                name,
                creatorId,
                welcomeMessage,
                topic,
                privateRoom);

            Append(fact);
            Apply(fact);
        }

        public string Name { get; private set; }

        public string Topic { get; private set; }

        public bool PrivateRoom { get; private set; }
        
        public bool Closed { get; private set; }
        
        public string WelcomeMessage { get; private set; }

        public Guid CreatorId { get; private set; }
        
        public string InviteCode { get; private set; }

        public IEnumerable<Guid> Owners { get { return _owners; } }

        public IEnumerable<Guid> Users { get { return _users; } }

        public IEnumerable<Guid> AllowedUsers { get { return _allowedUsers; } }

        public void Apply(RoomCreatedFact fact)
        {
            if (fact == null) 
                throw new ArgumentNullException("fact");

            Id = fact.AggregateRootId;
            Name = fact.Name;
            CreatorId = fact.CreatorId;
            WelcomeMessage = fact.Welcome;
            Topic = fact.Topic;
            PrivateRoom = fact.PrivateRoom;

            _owners.Add(fact.CreatorId);
            _allowedUsers.Add(fact.CreatorId);
        }

        public void UpdateWelcomeMessage(string newWelcomeMessage)
        {
            if (newWelcomeMessage == null)
                throw new ArgumentNullException("newWelcomeMessage");

            var fact = new RoomWelcomeMessageUpdatedFact(Id, newWelcomeMessage);

            Append(fact);
            Apply(fact);
        }

        public void Apply(RoomWelcomeMessageUpdatedFact fact)
        {
            if (fact == null)
                throw new ArgumentNullException("fact");

            WelcomeMessage = fact.NewWelcomeMessage;
        }

        public void UpdateTopic(string topic)
        {
            if (topic == null) 
                throw new ArgumentNullException("topic");

            var fact = new RoomTopicUpdatedFact(Id, topic);

            Append(fact);
            Apply(fact);
        }

        public void Apply(RoomTopicUpdatedFact fact)
        {
            if (fact == null) 
                throw new ArgumentNullException("fact");

            Topic = fact.NewTopic;
        }

        public void Join(User user)
        {
            if (user == null) 
                throw new ArgumentNullException("user");

            var fact = new UserJoinedFact(Id, user.Id);

            Append(fact);
            Apply(fact);
        }

        public void Apply(UserJoinedFact fact)
        {
            if (fact == null) 
                throw new ArgumentNullException("fact");

            _users.Add(fact.UserId);
        }

        public void Leave(User user)
        {
            if (user == null) 
                throw new ArgumentNullException("user");

            var fact = new UserLeftFact(Id, user.Id);

            Append(fact);
            Apply(fact);
        }

        public void Apply(UserLeftFact fact)
        {
            if (fact == null) 
                throw new ArgumentNullException("fact");

            _users.Remove(fact.UserId);
        }

        public void RegisterOwner(User owner)
        {
            if (owner == null) 
                throw new ArgumentNullException("owner");

            var fact = new OwnerRegisteredFact(Id, owner.Id);

            Append(fact);
            Apply(fact);
        }

        public void Apply(OwnerRegisteredFact fact)
        {
            if (fact == null) 
                throw new ArgumentNullException("fact");

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
            if (fact == null) 
                throw new ArgumentNullException("fact");

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
            if (fact == null) 
                throw new ArgumentNullException("fact");

            Closed = false;
        }

        public void AllowUser(User user)
        {
            if (user == null) 
                throw new ArgumentNullException("user");

            var fact = new UserAllowedFact(Id, user.Id);

            Append(fact);
            Apply(fact);
        }

        public void Apply(UserAllowedFact fact)
        {
            if (fact == null) 
                throw new ArgumentNullException("fact");

            _allowedUsers.Add(fact.UserId);
        }

        public void DeregisterOwner(User owner)
        {
            if (owner == null) 
                throw new ArgumentNullException("owner");

            var fact = new OwnerRemovedFact(Id, owner.Id);

            Append(fact);
            Apply(fact);
        }

        public void Apply(OwnerRemovedFact fact)
        {
            if (fact == null) 
                throw new ArgumentNullException("fact");

            _owners.Remove(fact.OwnerId);
        }

        public void UnallowUser(User user)
        {
            if (user == null) 
                throw new ArgumentNullException("user");

            var fact = new UserUnallowedFact(Id, user.Id);

            Append(fact);
            Apply(fact);
        }

        public void Apply(UserUnallowedFact fact)
        {
            if (fact == null) 
                throw new ArgumentNullException("fact");

            _allowedUsers.Remove(fact.UserId);
        }

        public void SetInviteCode(string inviteCode)
        {
            if (inviteCode == null) 
                throw new ArgumentNullException("inviteCode");

            var fact = new InviteCodeSetFact(Id, inviteCode);

            Append(fact);
            Apply(fact);
        }

        public void Apply(InviteCodeSetFact fact)
        {
            if (fact == null) 
                throw new ArgumentNullException("fact");

            InviteCode = fact.InviteCode;
        }
    }
}