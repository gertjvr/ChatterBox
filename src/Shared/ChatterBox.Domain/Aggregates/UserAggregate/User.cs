using System;
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
        
        public UserStatus Status { get; protected set; }
        
        public User(string name, string email, string hash, string salt, string hashedPassword, UserRole role = UserRole.User)
        {
            var fact = new UserCreatedFact
            {
                AggregateRootId = Guid.NewGuid(),
                Name = name,
                Email = email,
                Hash = hash,
                Salt = salt,
                HashedPassword = hashedPassword,
                UserRole = role,
                Status = UserStatus.Active,
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
            UserRole = fact.UserRole;
            Status = fact.Status;
            LastActivity = fact.LastActivity;
        }

        public void UpdateLastActivity(DateTimeOffset lastActivity)
        {
            var fact = new UserLastActivityUpdatedFact
            {
                AggregateRootId = Guid.NewGuid(),
                LastActivity = lastActivity,
            };

            Append(fact);
            Apply(fact);
        }

        public void Apply(UserLastActivityUpdatedFact fact)
        {
            LastActivity = fact.LastActivity;
        }

        public void ChangeUserName(string newUserName)
        {
            var fact = new UserNameChangedFact
            {
                AggregateRootId = Id,
                NewUserName = newUserName,
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
                UserRole = userRole,
            };

            Append(fact);
            Apply(fact);
        }

        public void Apply(UserRoleChangedFact fact)
        {
            UserRole = fact.UserRole;
        }

        public void ChangePassword(string newHashedPassword)
        {
            var fact = new UserPasswordChangedFact
            {
                AggregateRootId = Id,
                NewHashedPassword = newHashedPassword,
            };

            Append(fact);
            Apply(fact);
        }

        public void Apply(UserPasswordChangedFact fact)
        {
            HashedPassword = fact.NewHashedPassword;
        }

        public void ChangeSalt(string newSalt)
        {
            var fact = new UserSaltChangedFact
            {
                AggregateRootId = Id,
                NewSalt = newSalt,
            };

            Append(fact);
            Apply(fact);
        }

        public void Apply(UserSaltChangedFact fact)
        {
            Salt = fact.NewSalt;
        }

        public void ChangeStatus(UserStatus status)
        {
            var fact = new UserStatusChangedFact
            {
                AggregateRootId = Id,
                Status = Status,
            };

            Append(fact);
            Apply(fact);
        }

        public void Apply(UserStatusChangedFact fact)
        {
            Status = fact.Status;
        }
    }
}