using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Commands
{
    public class ChangeTopicCommand : IBusCommand
    {
        protected ChangeTopicCommand()
        {   
        }

        public ChangeTopicCommand(Guid roomId, Guid userId, string newTopic)
        {
            RoomId = roomId;
            UserId = userId;
            NewTopic = newTopic;
        }

        public Guid RoomId { get; protected set; }

        public Guid UserId { get; protected set; }

        public string NewTopic { get; protected set; }
    }
}