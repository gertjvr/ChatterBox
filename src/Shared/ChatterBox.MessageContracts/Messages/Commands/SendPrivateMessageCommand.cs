using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Messages.Commands
{
    public class SendPrivateMessageCommand : IBusCommand
    {
        protected SendPrivateMessageCommand()
        {   
        }

        public SendPrivateMessageCommand(string content, Guid targetUserId, Guid userId)
        {
            if (content == null) 
                throw new ArgumentNullException("content");

            if (targetUserId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "targetUserId");

            if (userId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "userId");

            Content = content;
            TargetUserId = targetUserId;
            UserId = userId;
        }

        public string Content { get; private set; }
        
        public Guid TargetUserId { get; private set; }

        public Guid UserId { get; private set; }
    }
}