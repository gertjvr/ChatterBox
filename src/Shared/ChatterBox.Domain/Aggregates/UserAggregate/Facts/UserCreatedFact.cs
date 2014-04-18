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
            UserStatus status, 
            DateTimeOffset lastActivity) 
            : base(aggregateRootId)
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

            Name = name;
            Email = email;
            Hash = hash;
            Salt = salt;
            HashedPassword = hashedPassword;
            UserRole = userRole;
            LastActivity = lastActivity;
            Status = status;
        }

        public string Name { get; private set; }

        public string Email { get; private set; }

        public string Hash { get; private set; }
        
        public string Salt { get; private set; }

        public string HashedPassword { get; private set; }

        public UserRole UserRole { get; private set; }
   
        public DateTimeOffset LastActivity { get; private set; }
        
        public UserStatus Status { get; private set; }
    }
}