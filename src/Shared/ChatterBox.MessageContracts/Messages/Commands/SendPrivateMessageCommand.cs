using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Messages.Commands
{
    public class SendPrivateMessageCommand : IBusCommand
    {
        protected SendPrivateMessageCommand()
        {   
        }

        public SendPrivateMessageCommand(string content, Guid targetUserId, Guid callingUserId)
        {
            if (content == null) 
                throw new ArgumentNullException("content");

            if (targetUserId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "targetUserId");

            if (callingUserId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "callingUserId");

            Content = content;
            TargetUserId = targetUserId;
            CallingUserId = callingUserId;
        }

        public string Content { get; private set; }
        
        public Guid TargetUserId { get; private set; }

        public Guid CallingUserId { get; private set; }
    }
}