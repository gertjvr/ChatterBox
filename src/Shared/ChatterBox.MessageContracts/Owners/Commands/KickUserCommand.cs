using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Owners.Commands
{
    public class KickUserCommand : IBusCommand
    {
        protected KickUserCommand()
        {   
        }
        
        public KickUserCommand(Guid targetUserId, Guid targetRoomId, Guid userId)
        {
            if (targetUserId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "targetUserId");

            if (targetRoomId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "targetRoomId");

            if (userId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "userId");

            TargetUserId = targetUserId;
            TargetRoomId = targetRoomId;
            UserId = userId;
        }

        public Guid TargetUserId { get; private set; }

        public Guid TargetRoomId { get; private set; }

        public Guid UserId { get; private set; }
    }
}