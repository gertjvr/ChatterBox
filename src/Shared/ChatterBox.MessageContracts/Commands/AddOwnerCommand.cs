using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Commands
{
    public class AddOwnerCommand : IBusCommand
    {
        protected AddOwnerCommand()
        {   
        }

        public AddOwnerCommand(Guid roomId, Guid userId)
        {
            RoomId = roomId;
            UserId = userId;
        }

        public Guid RoomId { get; protected set; }

        public Guid UserId { get; protected set; }
    }
}