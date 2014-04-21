using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Messages.Commands
{
    public class SendMessageCommand : IBusCommand
    {
        protected SendMessageCommand()
        {   
        }

        public SendMessageCommand(string content, Guid targetRoomId, Guid callingUserId)
        {
            if (content == null) 
                throw new ArgumentNullException("content");

            if (targetRoomId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "targetRoomId");

            if (callingUserId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "callingUserId");

            Content = content;

            TargetRoomId = targetRoomId;
            CallingUserId = callingUserId;
        }

        public string Content { get; private set; }

        public Guid TargetRoomId { get; private set; }

        public Guid CallingUserId { get; private set; }
    }
}