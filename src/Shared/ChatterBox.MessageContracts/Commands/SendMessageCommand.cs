using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Commands
{
    public class SendMessageCommand : IUserBusCommand
    {
        protected SendMessageCommand()
        {   
        }

        public SendMessageCommand(string content, Guid targetRoomId, Guid userId)
        {
            Content = content;

            TargetRoomId = targetRoomId;
            UserId = userId;
        }

        public string Content { get; protected set; }

        public Guid TargetRoomId { get; protected set; }

        public Guid UserId { get; protected set; }
    }
}