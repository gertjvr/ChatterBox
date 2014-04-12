using System;

namespace ChatterBox.MessageContracts.Commands
{
    public class RemoveOwnerCommand : IUserCommand
    {
        public RemoveOwnerCommand(Guid roomId, Guid ownerId, Guid userId)
        {
            RoomId = roomId;
            OwnerId = ownerId;
            UserId = userId;
        }

        public Guid RoomId { get; protected set; }

        public Guid OwnerId { get; protected set; }

        public Guid UserId { get; private set; }
    }
}