using System;

namespace ChatterBox.MessageContracts.Owners.Commands
{
    public class AllowUserCommand : IUserBusCommand
    {
        protected AllowUserCommand()
        {   
        }

        public AllowUserCommand(Guid targetRoomId, Guid targetUserId, Guid userId)
        {
            if (targetRoomId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "targetRoomId");

            if (targetUserId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "targetUserId");

            if (userId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "userId");

            TargetRoomId = targetRoomId;
            TargetUserId = targetUserId;
            UserId = userId;
        }

        public Guid TargetRoomId { get; protected set; }

        public Guid TargetUserId { get; protected set; }

        public Guid UserId { get; protected set; }
    }
}