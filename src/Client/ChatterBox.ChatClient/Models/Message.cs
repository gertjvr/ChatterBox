using System;

namespace ChatterBox.ChatClient.Models
{
    public class Message
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public User User { get; set; }
    }
}
