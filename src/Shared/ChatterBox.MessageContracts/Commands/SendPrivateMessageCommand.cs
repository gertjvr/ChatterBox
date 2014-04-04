using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Commands
{
    public class SendPrivateMessageCommand : IBusCommand
    {
        protected SendPrivateMessageCommand()
        {
        }

        public SendPrivateMessageCommand(Guid targetUserId, string content, Guid userId)
        {
            Content = content;
            TargetUserId = targetUserId;
            UserId = userId;
        }

        public Guid TargetUserId { get; set; }
        
        public string Content { get; set; }

        public Guid UserId { get; set; }
    }
}