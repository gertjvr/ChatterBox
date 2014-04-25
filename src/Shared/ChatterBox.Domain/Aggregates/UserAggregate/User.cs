using System;
using System.Collections.Generic;
using ChatterBox.Core.Extensions;
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
            string emailAddress,
            string hash,
            string salt,
            string hashedPassword,
            DateTimeOffset lastActivity,
            UserRole role = UserRole.User,
            UserStatus status = UserStatus.Active) 
            : this(
                Guid.NewGuid(), 
                name, 
                emailAddress, 
                hash, 
                salt,
                hashedPassword, 
                lastActivity, 
                role, 
                status)
        {
        }

        public User(
            Guid id,
            string name,
            string emailAddress,
            string hash,
            string salt,
            string hashedPassword,
            DateTimeOffset lastActivity,
            UserRole role = UserRole.User,
            UserStatus status = UserStatus.Active)
        {
            if (id == null)
                throw new ArgumentException(LanguageResources.GuidCannotBeEmpty, "id");

            if (name == null)
                throw new ArgumentNullException("name");

            if (emailAddress == null)
                throw new ArgumentNullException("emailAddress");

            if (hash == null)
                throw new ArgumentNullException("hash");

            if (salt == null)
                throw new ArgumentNullException("salt");

            if (hashedPassword == null)
                throw new ArgumentNullException("hashedPassword");

            var fact = new UserCreatedFact(
                id,
                name,
                emailAddress,
                hash,
                salt,
                hashedPassword,
                role,
                status,
                lastActivity);

            Append(fact);
            Apply(fact);
        }

        public string Name { get; private set; }

        public string EmailAddress { get; private set; }

        public string Hash { get; private set; }

        public string Salt { get; private set; }

        public string HashedPassword { get; private set; }

        public UserRole Role { get; private set; }

        public DateTimeOffset LastActivity { get; private set; }

        public UserStatus Status { get; private set; }

        public IEnumerable<PrivateMessage> PrivateMessages()
        {
            return _privateMessages;
        }

        public IEnumerable<Guid> ConnectedClients()
        {
            return _connectedClients;
        }

        public void Apply(UserCreatedFact fact)
        {
            if (fact == null)
                throw new ArgumentNullException("fact");

            Id = fact.AggregateRootId;
            Name = fact.Name;
            EmailAddress = fact.Email;
            Hash = fact.Hash;
            Salt = fact.Salt;
            HashedPassword = fact.HashedPassword;
            Role = fact.UserRole;
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

            var fact = new UserEmailAddressUpdatedFact(Id, newEmailAddress);

            Append(fact);
            Apply(fact);
        }

        public void Apply(UserEmailAddressUpdatedFact fact)
        {
            if (fact == null)
                throw new ArgumentNullException("fact");

            EmailAddress = fact.NewEmailAddress;
        }

        public void UpdateRole(UserRole userRole)
        {
            var fact = new UserRoleUpdatedFact(Id, userRole);

            Append(fact);
            Apply(fact);
        }

        public void Apply(UserRoleUpdatedFact fact)
        {
            if (fact == null)
                throw new ArgumentNullException("fact");

            Role = fact.UserRole;
        }

        public void UpdatePassword(string newPassword)
        {
            if (newPassword == null)
                throw new ArgumentNullException("newPassword");

            var hashedPassword = newPassword.ToSha256(Salt);

            var fact = new UserPasswordUpdatedFact(Id, hashedPassword);

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

        public void ReceivePrivateMessage(string content, User user, DateTimeOffset receivedAt)
        {
            if (content == null) 
                throw new ArgumentNullException("content");

            if (user == null)
                throw new ArgumentNullException("user");

            var fact = new PrivateMessageReceivedFact(Id, content, user.Id, receivedAt);

            Append(fact);
            Apply(fact);
        }

        public void Apply(PrivateMessageReceivedFact fact)
        {
            if (fact == null) 
                throw new ArgumentNullException("fact");

            _privateMessages.Add(new PrivateMessage(fact.Content, fact.UserId, fact.ReceivedAt));
        }

        public void RegisterClient(Client client)
        {
            if (client == null) 
                throw new ArgumentNullException("client");

            var fact = new ConnectedClientRegisteredFact(Id, client.Id);

            Append(fact);
            Apply(fact);
        }

        public void Apply(ConnectedClientRegisteredFact fact)
        {
            if (fact == null) 
                throw new ArgumentNullException("fact");

            _connectedClients.Add(fact.ClientId);
        }
        
        public void DeregisterClient(Client client)
        {
            if (client == null) 
                throw new ArgumentNullException("client");

            var fact = new ConnectedClientDeregisteredFact(Id, client.Id);

            Append(fact);
            Apply(fact);
        }

        public void Apply(ConnectedClientDeregisteredFact fact)
        {
            if (fact == null) 
                throw new ArgumentNullException("fact");

            _connectedClients.Remove(fact.ClientId);
        }
    }
}