using System;

namespace ChatterBox.MessageContracts.Owners.Commands
{
    public class ChangeTopicCommand : IUserBusCommand
    {
        protected ChangeTopicCommand()
        {   
        }
        
        public ChangeTopicCommand(Guid targetRoomId, string newTopic, Guid userId)
        {
            if (targetRoomId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "targetRoomId");
            
            if (newTopic == null) 
                throw new ArgumentNullException("newTopic");

            if (userId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "userId");

            TargetRoomId = targetRoomId;
            NewTopic = newTopic;
            UserId = userId;
        }

        public Guid TargetRoomId { get; private set; }

        public string NewTopic { get; private set; }
        
        public Guid UserId { get; private set; }
    }
}