using System;
using System.Collections.Generic;

namespace ChatterBox.MessageContracts.Dtos
{
    public class RoomDto : IDto
    {
        public RoomDto(string name, int count, bool privateRoom, string topic, bool closed, string welcomeMessage,
            IEnumerable<UserDto> users, IEnumerable<UserDto> owners, IEnumerable<MessageDto> recentMessages)
        {
            if (name == null) 
                throw new ArgumentNullException("name");
            
            if (topic == null) 
                throw new ArgumentNullException("topic");
            
            if (welcomeMessage == null) 
                throw new ArgumentNullException("welcomeMessage");
            
            if (users == null) 
                throw new ArgumentNullException("users");
            
            if (owners == null) 
                throw new ArgumentNullException("owners");
            
            if (recentMessages == null) 
                throw new ArgumentNullException("recentMessages");

            Name = name;
            Count = count;
            PrivateRoom = privateRoom;
            Topic = topic;
            Closed = closed;
            WelcomeMessage = welcomeMessage;
            Users = users;
            Owners = owners;
            RecentMessages = recentMessages;
        }

        public string Name { get; private set; }
        public int Count { get; private set; }
        public bool PrivateRoom { get; private set; }
        public string Topic { get; private set; }
        public bool Closed { get; private set; }
        public string WelcomeMessage { get; private set; }
        public IEnumerable<UserDto> Users { get; private set; }
        public IEnumerable<UserDto> Owners { get; private set; }
        public IEnumerable<MessageDto> RecentMessages { get; private set; }
    }
}