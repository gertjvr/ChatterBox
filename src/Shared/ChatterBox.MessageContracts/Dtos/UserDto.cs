using System;

namespace ChatterBox.MessageContracts.Dtos
{
    public class UserDto : IDto
    {
        public string Name { get; set; }
        public string Hash { get; set; }
        public int Status { get; set; }
        public string Note { get; set; }
        public DateTimeOffset LastActivity { get; set; }
        public int UserRole { get; set; }
    }
}
