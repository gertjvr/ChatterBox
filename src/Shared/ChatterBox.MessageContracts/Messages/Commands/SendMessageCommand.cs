using System;

namespace ChatterBox.MessageContracts.Messages.Commands
{
    public class SendMessageCommand : IUserBusCommand
    {
        protected SendMessageCommand()
        {   
        }

        public SendMessageCommand(string content, Guid targetRoomId, Guid userId)
        {
            if (content == null) 
                throw new ArgumentNullException("content");

            if (targetRoomId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "targetRoomId");

            if (userId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "userId");

            Content = content;

            TargetRoomId = targetRoomId;
            UserId = userId;
        }

        public string Content { get; private set; }

        public Guid TargetRoomId { get; private set; }

        public Guid UserId { get; private set; }
    }
}