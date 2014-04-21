using System;
using System.Collections.Generic;

namespace ChatterBox.MessageContracts.Dtos
{
    public class RoomDto : IDto
    {
        public RoomDto(string name, int count, bool @private, string topic, bool closed, string welcome,
            IEnumerable<UserDto> contacts, IEnumerable<UserDto> owners, IEnumerable<MessageDto> recentMessages)
        {
            if (name == null) 
                throw new ArgumentNullException("name");
            
            if (topic == null) 
                throw new ArgumentNullException("topic");
            
            if (welcome == null) 
                throw new ArgumentNullException("welcome");
            
            if (contacts == null) 
                throw new ArgumentNullException("contacts");
            
            if (owners == null) 
                throw new ArgumentNullException("owners");
            
            if (recentMessages == null) 
                throw new ArgumentNullException("recentMessages");

            Name = name;
            Count = count;
            Private = @private;
            Topic = topic;
            Closed = closed;
            Welcome = welcome;
            Contacts = contacts;
            Owners = owners;
            RecentMessages = recentMessages;
        }

        public string Name { get; private set; }
        public int Count { get; private set; }
        public bool Private { get; private set; }
        public string Topic { get; private set; }
        public bool Closed { get; private set; }
        public string Welcome { get; private set; }
        public IEnumerable<UserDto> Contacts { get; private set; }
        public IEnumerable<UserDto> Owners { get; private set; }
        public IEnumerable<MessageDto> RecentMessages { get; private set; }
    }
}