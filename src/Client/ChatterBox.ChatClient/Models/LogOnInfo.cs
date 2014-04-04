using System.Collections.Generic;

namespace ChatterBox.ChatClient.Models
{
    public class LogOnInfo
    {
        public User User { get; set; }
        public IEnumerable<Room> Rooms { get; set; }

        public LogOnInfo()
        {
            Rooms = new List<Room>();
        }
    }
}
