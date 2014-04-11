using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Commands
{
    public class SendMessageCommand : IBusCommand
    {
        protected SendMessageCommand()
        {   
        }

        public SendMessageCommand(DateTimeOffset createdAt, string content, Guid roomId, Guid userId)
        {
            CreatedAt = createdAt;
            Content = content;
            RoomId = roomId;
            UserId = userId;
        }

        public DateTimeOffset CreatedAt { get; protected set; }

        public string Content { get; protected set; }

        public Guid RoomId { get; protected set; }

        public Guid UserId { get; protected set; }
    }
}