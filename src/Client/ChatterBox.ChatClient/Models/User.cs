using System;

namespace ChatterBox.ChatClient.Models
{
    public class User
    {
        public string Name { get; set; }
        public string Hash { get; set; }
        public UserStatus Status { get; set; }
        public string Note { get; set; }
        public DateTimeOffset LastActivity { get; set; }
        public UserRole UserRole { get; set; }
    }
}
