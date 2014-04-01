using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Owners.Commands
{
    public class ChangeTopicCommand : IBusCommand
    {
        public Guid RoomId { get; set; }

        public Guid UserId { get; set; }

        public string NewTopic { get; set; }
    }
}