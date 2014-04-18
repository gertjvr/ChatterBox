using System;

namespace ChatterBox.MessageContracts.Dtos
{
    public class MessageDto : IDto
    {
        public MessageDto(Guid id, string content, DateTimeOffset createdAt, UserDto user)
        {
            Id = id;
            Content = content;
            CreatedAt = createdAt;
            User = user;
        }

        public Guid Id { get; private set; }
        public string Content { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public UserDto User { get; private set; }
    }
}