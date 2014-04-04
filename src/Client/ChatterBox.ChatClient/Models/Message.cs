using System;

namespace ChatterBox.ChatClient.Models
{
    public class Message
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public DateTimeOffset When { get; set; }
        public User User { get; set; }
    }
}
