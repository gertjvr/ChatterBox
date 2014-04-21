using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Owners.Commands
{
    public class ChangeTopicCommand : IBusCommand
    {
        protected ChangeTopicCommand()
        {   
        }
        
        public ChangeTopicCommand(Guid targetRoomId, string newTopic, Guid callingUserId)
        {
            if (targetRoomId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "targetRoomId");
            
            if (newTopic == null) 
                throw new ArgumentNullException("newTopic");

            if (callingUserId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "callingUserId");

            TargetRoomId = targetRoomId;
            NewTopic = newTopic;
            CallingUserId = callingUserId;
        }

        public Guid TargetRoomId { get; private set; }

        public string NewTopic { get; private set; }
        
        public Guid CallingUserId { get; private set; }
    }
}