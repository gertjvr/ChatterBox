using System;
using ChatterBox.Core.Extentions;
using ChatterBox.Core.Infrastructure.Entities;
using Domain.Aggregates.UserAggregate.Facts;

namespace Domain.Aggregates.UserAggregate
{
    [Serializable]
    public class User : AggregateRoot
    {
        protected User()
        {
        }

        public string Name { get; set; }

        public string Email { get; protected set; }

        public string Hash { get; set; }

        public string Salt { get; set; }

        public string HashedPassword { get; set; }

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
        }

        public void Apply(ChangeUserNameFact fact)
        {
            Name = fact.NewUserName;
        }

        public void Apply(SetUserPasswordFact fact)
        {
            HashedPassword = fact.HashedPassword;
        }

        public void ChangeUserName(string newUserName)
        {
            var fact = new ChangeUserNameFact
            {
                AggregateRootId = Id,
                NewUserName = newUserName
            };

            Append(fact);
            Apply(fact);
        }

        public void SetUserPassword(string password)
        {
            var fact = new SetUserPasswordFact
            {
                AggregateRootId = Id,
                HashedPassword = password.ToSha256(Salt)
            };

            Append(fact);
            Apply(fact);
        }
    }
}