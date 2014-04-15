using System;

namespace ChatterBox.MessageContracts.Commands
{
    public class AllowUserCommand : IUserBusCommand
    {
        public AllowUserCommand(Guid targetRoomId, Guid targetUserId, Guid userId)
        {
            TargetRoomId = targetRoomId;
            TargetUserId = targetUserId;
            UserId = userId;
        }

        public Guid TargetRoomId { get; protected set; }

        public Guid TargetUserId { get; protected set; }

        public Guid UserId { get; protected set; }
    }
}