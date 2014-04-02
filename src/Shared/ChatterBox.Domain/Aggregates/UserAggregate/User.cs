﻿using System;
using ChatterBox.Core.Extentions;
using ChatterBox.Core.Infrastructure;
using ChatterBox.Core.Infrastructure.Entities;
using ChatterBox.Domain.Aggregates.UserAggregate.Facts;

namespace ChatterBox.Domain.Aggregates.UserAggregate
{
    [Serializable]
    public class User : AggregateRoot
    {
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

        public User(string name, string email, string hash, string salt, string hashedPassword)
        {
            var fact = new UserCreatedFact
            {
                AggregateRootId = Guid.NewGuid(),
                Name = name,
                Email = email,
                Hash = hash,
                Salt = salt,
                HashedPassword = hashedPassword,
                LastActivity = DateTimeHelper.UtcNow,
            };

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
            LastActivity = fact.LastActivity;
        }

        public void ChangeUserName(string newUserName)
        {
            var fact = new UserNameChangedFact
            {
                AggregateRootId = Id,
                NewUserName = newUserName
            };

            Append(fact);
            Apply(fact);
        }

        public void Apply(UserNameChangedFact fact)
        {
            Name = fact.NewUserName;
        }

        public void ChangeUserRole(UserRole userRole)
        {
            var fact = new UserRoleChangedFact
            {
                AggregateRootId = Id,
                UserRole = userRole
            };

            Append(fact);
            Apply(fact);
        }

        public void Apply(UserRoleChangedFact fact)
        {
            UserRole = fact.UserRole;
        }

        public void SetUserPassword(string password)
        {
            var fact = new UserPasswordChangedFact
            {
                AggregateRootId = Id,
                NewHashedPassword = password.ToSha256(Salt)
            };

            Append(fact);
            Apply(fact);
        }

        public void Apply(UserPasswordChangedFact changedFact)
        {
            HashedPassword = changedFact.NewHashedPassword;
        }
    }
}