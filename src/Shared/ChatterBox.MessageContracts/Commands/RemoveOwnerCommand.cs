using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Commands
{
    public class RemoveOwnerCommand : IBusCommand
    {
        protected RemoveOwnerCommand()
        {   
        }

        public RemoveOwnerCommand(Guid roomId, Guid ownerId)
        {
            RoomId = roomId;
            OwnerId = ownerId;
        }

        public Guid RoomId { get; protected set; }

        public Guid OwnerId { get; protected set; }
    }
}