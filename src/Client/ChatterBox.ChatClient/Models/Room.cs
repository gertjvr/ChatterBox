using System;
using System.Collections.Generic;

namespace ChatterBox.ChatClient.Models
{
    public class Room
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public bool Private { get; set; }
        public string Topic { get; set; }
        public bool Closed { get; set; }
        public string Welcome { get; set; }
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<User> Owners { get; set; }
        public IEnumerable<Message> RecentMessages { get; set; }
    }
}
