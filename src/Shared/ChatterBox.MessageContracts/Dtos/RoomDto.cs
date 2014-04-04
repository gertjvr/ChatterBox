using System;
using System.Collections.Generic;

namespace ChatterBox.MessageContracts.Dtos
{
    public class RoomDto : IDto
    {
        public string Name { get; set; }
        public int Count { get; set; }
        public bool Private { get; set; }
        public string Topic { get; set; }
        public bool Closed { get; set; }
        public string Welcome { get; set; }
        public IEnumerable<UserDto> Contacts { get; set; }
        public IEnumerable<UserDto> Owners { get; set; }
        public IEnumerable<MessageDto> RecentMessages { get; set; }
    }
}
