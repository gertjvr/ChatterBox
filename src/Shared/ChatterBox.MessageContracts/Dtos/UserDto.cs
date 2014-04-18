using System;

namespace ChatterBox.MessageContracts.Dtos
{
    public class UserDto : IDto
    {
        public UserDto(string name, string hash, DateTimeOffset lastActivity, int status, int userRole)
        {
            if (name == null) 
                throw new ArgumentNullException("name");

            if (hash == null) 
                throw new ArgumentNullException("hash");

            Name = name;
            Hash = hash;
            Status = status;
            LastActivity = lastActivity;
            UserRole = userRole;
        }

        public string Name { get; private set; }
        public string Hash { get; private set; }
        public int Status { get; private set; }
        public DateTimeOffset LastActivity { get; private set; }
        public int UserRole { get; private set; }
    }
}
