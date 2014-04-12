using System;
using System.Collections.Generic;
using ChatterBox.Core.Infrastructure.Entities;
using ChatterBox.Domain.Aggregates.UserAggregate.Facts;

namespace ChatterBox.Domain.Aggregates.UserAggregate
{
    [Serializable]
    public class User : AggregateRoot
    {
        private readonly List<PrivateMessage> _privateMessages = new List<PrivateMessage>(); 

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
                Guid.NewGuid(),
                lastActivity);

            Append(fact);
            Apply(fact);
        }

        public void Apply(UserLastActivityUpdatedFact fact)
        {
            LastActivity = fact.LastActivity;
        }

        public void ChangeUserName(string newUserName)
        {
            var fact = new UserNameChangedFact(
                Id,
                newUserName);

            Append(fact);
            Apply(fact);
        }

        public void Apply(UserNameChangedFact fact)
        {
            Name = fact.NewUserName;
        }

        public void ChangeUserRole(UserRole userRole)
        {
            var fact = new UserRoleChangedFact(
                Id,
                userRole);

            Append(fact);
            Apply(fact);
        }

        public void Apply(UserRoleChangedFact fact)
        {
            UserRole = fact.UserRole;
        }

        public void ChangePassword(string newHashedPassword)
        {
            var fact = new UserPasswordChangedFact(
                Id,
                newHashedPassword);

            Append(fact);
            Apply(fact);
        }

        public void Apply(UserPasswordChangedFact fact)
        {
            HashedPassword = fact.NewHashedPassword;
        }

        public void ChangeSalt(string newSalt)
        {
            var fact = new UserSaltChangedFact(
                Id,
                newSalt);

            Append(fact);
            Apply(fact);
        }

        public void Apply(UserSaltChangedFact fact)
        {
            Salt = fact.NewSalt;
        }

        public void ChangeStatus(UserStatus status)
        {
            var fact = new UserStatusChangedFact(
                Id,
                Status);

            Append(fact);
            Apply(fact);
        }

        public void Apply(UserStatusChangedFact fact)
        {
            Status = fact.Status;
        }

        public void ReceivePrivateMessage(string content, Guid userId, DateTimeOffset receivedAt)
        {
            var fact = new PrivateMessageReceivedFact(
                content, 
                userId,
                receivedAt);

            Append(fact);
            Apply(fact);
        }

        public void Apply(PrivateMessageReceivedFact fact)
        {
            _privateMessages.Add(new PrivateMessage(fact.Content, fact.UserId, fact.ReceivedAt));
        }    
    }
}