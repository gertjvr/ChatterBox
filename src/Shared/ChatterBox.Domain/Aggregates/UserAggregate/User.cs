using System;
using System.Collections.Generic;
using ChatterBox.Core.Infrastructure.Entities;
using ChatterBox.Domain.Aggregates.ClientAggregate;
using ChatterBox.Domain.Aggregates.UserAggregate.Facts;

namespace ChatterBox.Domain.Aggregates.UserAggregate
{
    [Serializable]
    public class User : AggregateRoot
    {
        private readonly List<PrivateMessage> _privateMessages = new List<PrivateMessage>();
        private readonly List<Guid> _connectedClients = new List<Guid>();

        protected User()
        {
        }

        public string Name { get; protected set; }

        public string Email { get; protected set; }

        public string Hash { get; protected set; }

        public string Salt { get; protected set; }

        public string HashedPassword { get; protected set; }

        public UserRole UserRole { get; protected set; }

        public DateTimeOffset LastActivity { get; protected set; }
        
        public UserStatus Status { get; protected set; }

        public IEnumerable<PrivateMessage> PrivateMessages { get { return _privateMessages; } }
        
        public bool IsAdmin { get { return UserRole == UserRole.Admin; } }

        public bool IsBanned { get { return UserRole == UserRole.Banned; } }
        
        public IEnumerable<Guid> ConnectedClients { get { return _connectedClients; } }

        public User(string name, string email, string hash, string salt, string hashedPassword, DateTimeOffset lastActivity, UserRole role = UserRole.User, UserStatus status = UserStatus.Active)
        {
            var fact = new UserCreatedFact(
                Guid.NewGuid(),
                name,
                email,
                hash,
                salt,
                hashedPassword,
                role,
                status,
                lastActivity);

            Append(fact);
            Apply(fact);
        }

        public void Apply(UserCreatedFact fact)
        {
            Id = fact.AggregateRootId;
            Name = fact.Name;
            Email = fact.Email;
            Hash = fact.Hash;
            Salt = fact.Salt;
            HashedPassword = fact.HashedPassword;
            UserRole = fact.UserRole;
            Status = fact.Status;
            LastActivity = fact.LastActivity;
        }

        public void UpdateLastActivity(DateTimeOffset lastActivity)
        {
            var fact = new UserLastActivityUpdatedFact(
                Id,
                lastActivity);

            Append(fact);
            Apply(fact);
        }

        public void Apply(UserLastActivityUpdatedFact fact)
        {
            LastActivity = fact.LastActivity;
        }

        public void UpdateUserName(string newUserName)
        {
            var fact = new UserNameUpdatedFact(
                Id,
                newUserName);

            Append(fact);
            Apply(fact);
        }

        public void Apply(UserNameUpdatedFact fact)
        {
            Name = fact.NewUserName;
        }

        public void UpdateUserRole(UserRole userRole)
        {
            var fact = new UserRoleUpdatedFact(
                Id,
                userRole);

            Append(fact);
            Apply(fact);
        }

        public void Apply(UserRoleUpdatedFact fact)
        {
            UserRole = fact.UserRole;
        }

        public void UpdatePassword(string newHashedPassword)
        {
            var fact = new UserPasswordUpdatedFact(
                Id,
                newHashedPassword);

            Append(fact);
            Apply(fact);
        }

        public void Apply(UserPasswordUpdatedFact fact)
        {
            HashedPassword = fact.NewHashedPassword;
        }

        public void UpdateSalt(string newSalt)
        {
            var fact = new UserSaltUpdatedFact(
                Id,
                newSalt);

            Append(fact);
            Apply(fact);
        }

        public void Apply(UserSaltUpdatedFact fact)
        {
            Salt = fact.NewSalt;
        }

        public void UpdateStatus(UserStatus status)
        {
            var fact = new UserStatusUpdatedFact(Id, status);

            Append(fact);
            Apply(fact);
        }

        public void Apply(UserStatusUpdatedFact fact)
        {
            Status = fact.Status;
        }

        public void ReceivePrivateMessage(string content, Guid userId, DateTimeOffset receivedAt)
        {
            var fact = new PrivateMessageReceivedFact(content, userId, receivedAt);

            Append(fact);
            Apply(fact);
        }

        public void Apply(PrivateMessageReceivedFact fact)
        {
            _privateMessages.Add(new PrivateMessage(fact.Content, fact.UserId, fact.ReceivedAt));
        }

        public void RemoveConnectedClient(Client client)
        {
            var fact = new ConnectedClientRemovedFact(Id, client.Id);

            Append(fact);
            Apply(fact);
        }

        public void Apply(ConnectedClientRemovedFact fact)
        {
            _connectedClients.Remove(fact.ClientId);
        }
    }
}