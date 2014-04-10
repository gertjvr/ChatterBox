using System;
using ChatterBox.Core.Infrastructure.Facts;

namespace ChatterBox.Domain.Aggregates.UserAggregate.Facts
{
    public class UserCreatedFact : FactAbout<User>
    {
        public UserCreatedFact(
            Guid aggregateRootId, 
            string name, 
            string email, 
            string hash, 
            string salt, 
            string hashedPassword, 
            UserRole userRole, 
            DateTimeOffset lastActivity, 
            UserStatus status) 
            : base(aggregateRootId)
        {
            Name = name;
            Email = email;
            Hash = hash;
            Salt = salt;
            HashedPassword = hashedPassword;
            UserRole = userRole;
            LastActivity = lastActivity;
            Status = status;
        }

        public string Name { get; protected set; }

        public string Email { get; protected set; }

        public string Hash { get; protected set; }
        
        public string Salt { get; protected set; }

        public string HashedPassword { get; protected set; }

        public UserRole UserRole { get; protected set; }
   
        public DateTimeOffset LastActivity { get; protected set; }
        
        public UserStatus Status { get; protected set; }
    }
}