using System;

namespace ChatterBox.MessageContracts.Commands
{
    public class RemoveOwnerCommand : IUserBusCommand
    {
        public RemoveOwnerCommand(Guid targetRoomId, Guid targetUserId, Guid userId)
        {
            TargetRoomId = targetRoomId;
            TargetUserId = targetUserId;
            UserId = userId;
        }

        public Guid TargetRoomId { get; protected set; }

        public Guid TargetUserId { get; protected set; }

        public Guid UserId { get; private set; }
    }
}