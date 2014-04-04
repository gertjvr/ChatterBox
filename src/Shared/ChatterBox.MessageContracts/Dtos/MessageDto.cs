using System;

namespace ChatterBox.MessageContracts.Dtos
{
    public class MessageDto : IDto
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public UserDto User { get; set; }
    }
}