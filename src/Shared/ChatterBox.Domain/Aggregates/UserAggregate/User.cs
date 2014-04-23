using System;
using System.Collections.Generic;
using ChatterBox.Core.Infrastructure.Entities;
using ChatterBox.Domain.Aggregates.ClientAggregate;
using ChatterBox.Domain.Aggregates.UserAggregate.Facts;
using ChatterBox.Domain.Properties;

namespace ChatterBox.Domain.Aggregates.UserAggregate
{
    public class User : AggregateRoot
    {
        private readonly List<Guid> _connectedClients = new List<Guid>();
        private readonly List<PrivateMessage> _privateMessages = new List<PrivateMessage>();

        protected User()
        {   
        }

        public User(
            string name,
            string email,
            string hash,
            string salt,
            string hashedPassword,
            DateTimeOffset lastActivity,
            UserRole userRole = UserRole.User,
            UserStatus status = UserStatus.Active)
        {
            if (name == null)
                throw new ArgumentNullException("name");

            if (email == null)
                throw new ArgumentNullException("email");

            if (hash == null)
                throw new ArgumentNullException("hash");

            if (salt == null)
                throw new ArgumentNullException("salt");

            if (hashedPassword == null) 
                throw new ArgumentNullException("hashedPassword");

            var fact = new UserCreatedFact(
                Guid.NewGuid(),
                name,
                email,
                hash,
                salt,
                hashedPassword,
                userRole,
                status,
                lastActivity);

            Append(fact);
            Apply(fact);
        }

        public string Name { get; private set; }

        public string Email { get; private set; }

        public string Hash { get; private set; }

        public string Salt { get; private set; }

        public string HashedPassword { get; private set; }

        public UserRole UserRole { get; private set; }

        public DateTimeOffset LastActivity { get; private set; }

        public UserStatus Status { get; private set; }

        public IEnumerable<PrivateMessage> PrivateMessages
        {
            get { return _privateMessages; }
        }

        public bool IsAdmin
        {
            get { return UserRole == UserRole.Admin; }
        }

        public bool IsBanned
        {
            get { return UserRole == UserRole.Banned; }
        }

        public IEnumerable<Guid> ConnectedClients
        {
            get { return _connectedClients; }
        }

        public void Apply(UserCreatedFact fact)
        {
            if (fact == null)
                throw new ArgumentNullException("fact");

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
            var fact = new UserLastActivityUpdatedFact(Id, lastActivity);

            Append(fact);
            Apply(fact);
        }

        public void Apply(UserLastActivityUpdatedFact fact)
        {
            if (fact == null) 
                throw new ArgumentNullException("fact");

            LastActivity = fact.LastActivity;
        }

        public void UpdateUserName(string newUserName)
        {
            if (newUserName == null) 
                throw new ArgumentNullException("newUserName");

            var fact = new UserNameUpdatedFact(Id, newUserName);

            Append(fact);
            Apply(fact);
        }

        public void Apply(UserNameUpdatedFact fact)
        {
            if (fact == null)
                throw new ArgumentNullException("fact");

            Name = fact.NewUserName;
        }

        public void UpdateEmailAddress(string newEmailAddress)
        {
            if (newEmailAddress == null)
                throw new ArgumentNullException("newEmailAddress");

            var fact = new EmailAddressUpdatedFact(Id, newEmailAddress);

            Append(fact);
            Apply(fact);
        }

        public void Apply(EmailAddressUpdatedFact fact)
        {
            if (fact == null)
                throw new ArgumentNullException("fact");

            Name = fact.NewEmailAddress;
        }

        public void UpdateUserRole(UserRole userRole)
        {
            var fact = new UserRoleUpdatedFact(Id, userRole);

            Append(fact);
            Apply(fact);
        }

        public void Apply(UserRoleUpdatedFact fact)
        {
            if (fact == null)
                throw new ArgumentNullException("fact");

            UserRole = fact.UserRole;
        }

        public void UpdatePassword(string newHashedPassword)
        {
            if (newHashedPassword == null) 
                throw new ArgumentNullException("newHashedPassword");

            var fact = new UserPasswordUpdatedFact(Id, newHashedPassword);

            Append(fact);
            Apply(fact);
        }

        public void Apply(UserPasswordUpdatedFact fact)
        {
            if (fact == null)
                throw new ArgumentNullException("fact");

            HashedPassword = fact.NewHashedPassword;
        }

        public void UpdateSalt(string newSalt)
        {
            if (newSalt == null) 
                throw new ArgumentNullException("newSalt");

            var fact = new UserSaltUpdatedFact(Id, newSalt);

            Append(fact);
            Apply(fact);
        }

        public void Apply(UserSaltUpdatedFact fact)
        {
            if (fact == null)
                throw new ArgumentNullException("fact");

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
            if (fact == null)
                throw new ArgumentNullException("fact");

            Status = fact.Status;
        }

        public void ReceivePrivateMessage(string content, Guid userId, DateTimeOffset receivedAt)
        {
            if (content == null) 
                throw new ArgumentNullException("content");

            if (userId == Guid.Empty)
                throw new ArgumentException(LanguageResources.GuidCannotBeEmpty, "userId");

            var fact = new PrivateMessageReceivedFact(Id, content, userId, receivedAt);

            Append(fact);
            Apply(fact);
        }

        public void Apply(PrivateMessageReceivedFact fact)
        {
            if (fact == null) 
                throw new ArgumentNullException("fact");

            _privateMessages.Add(new PrivateMessage(fact.Content, fact.UserId, fact.ReceivedAt));
        }

        public void RemoveConnectedClient(Client client)
        {
            if (client == null) 
                throw new ArgumentNullException("client");

            var fact = new ConnectedClientRemovedFact(Id, client.Id);

            Append(fact);
            Apply(fact);
        }

        public void Apply(ConnectedClientRemovedFact fact)
        {
            if (fact == null) 
                throw new ArgumentNullException("fact");

            _connectedClients.Remove(fact.ClientId);
        }
    }
}