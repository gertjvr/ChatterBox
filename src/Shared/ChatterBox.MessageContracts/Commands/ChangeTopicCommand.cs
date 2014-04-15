using System;

namespace ChatterBox.MessageContracts.Commands
{
    public class ChangeTopicCommand : IUserBusCommand
    {
        public ChangeTopicCommand(Guid roomId, string newTopic, Guid userId)
        {
            RoomId = roomId;
            NewTopic = newTopic;
            UserId = userId;
        }

        public Guid RoomId { get; protected set; }

        public string NewTopic { get; protected set; }
        
        public Guid UserId { get; protected set; }
    }
}