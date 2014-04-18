using System;

namespace ChatterBox.MessageContracts.Owners.Commands
{
    public class CloseRoomCommand : IUserBusCommand
    {
        protected CloseRoomCommand()
        {   
        }

        public CloseRoomCommand(Guid targetRoomId, Guid userId)
        {
            if (targetRoomId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "targetRoomId");

            if (userId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "userId");

            TargetRoomId = targetRoomId;
            UserId = userId;
        }

        public Guid TargetRoomId { get; private set; }

        public Guid UserId { get; private set; }
    }
}