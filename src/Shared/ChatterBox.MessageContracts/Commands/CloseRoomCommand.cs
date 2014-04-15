using System;

namespace ChatterBox.MessageContracts.Commands
{
    public class CloseRoomCommand : IUserBusCommand
    {
        public CloseRoomCommand(Guid targetRoomId, Guid userId)
        {
            TargetRoomId = targetRoomId;
            UserId = userId;
        }

        public Guid TargetRoomId { get; protected set; }

        public Guid UserId { get; protected set; }
    }
}