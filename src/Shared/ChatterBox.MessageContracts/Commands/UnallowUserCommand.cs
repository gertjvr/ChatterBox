using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Commands
{
    public class UnallowUserCommand : IBusCommand
    {
        protected UnallowUserCommand()
        {   
        }

        public UnallowUserCommand(Guid roomId, Guid userId)
        {
            RoomId = roomId;
            UserId = userId;
        }

        public Guid RoomId { get; protected set; }

        public Guid UserId { get; protected set; }
    }
}