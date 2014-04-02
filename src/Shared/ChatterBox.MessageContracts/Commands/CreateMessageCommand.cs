using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Commands
{
    public class CreateMessageCommand : IBusCommand
    {
        protected CreateMessageCommand()
        {
        }

        public CreateMessageCommand(Guid roomId, Guid userId, string content)
        {
            RoomId = roomId;
            UserId = userId;
            Content = content;
        }

        public Guid RoomId { get; set; }

        public Guid UserId { get; set; }

        public string Content { get; set; }
    }
}