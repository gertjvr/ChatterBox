using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Commands
{
    public class SendMessageCommand : IBusCommand
    {
        protected SendMessageCommand()
        {   
        }

        public SendMessageCommand(string content, Guid roomId, Guid userId)
        {
            Content = content;
            RoomId = roomId;
            UserId = userId;
        }

        public string Content { get; protected set; }

        public Guid RoomId { get; protected set; }

        public Guid UserId { get; protected set; }

    }
}