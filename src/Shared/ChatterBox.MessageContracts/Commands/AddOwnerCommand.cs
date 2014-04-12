using System;

namespace ChatterBox.MessageContracts.Commands
{
    public class AddOwnerCommand : IUserCommand
    {
        public AddOwnerCommand(Guid roomId, Guid targetUserId, Guid userId)
        {
            RoomId = roomId;
            TargetUserId = targetUserId;
            UserId = userId;
        }

        public Guid RoomId { get; protected set; }
        
        public Guid TargetUserId { get; protected set; }

        public Guid UserId { get; protected set; }
    }
}