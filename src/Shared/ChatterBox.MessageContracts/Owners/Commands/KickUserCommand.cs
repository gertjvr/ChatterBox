using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Owners.Commands
{
    public class KickUserCommand : IBusCommand
    {
        protected KickUserCommand()
        {   
        }
        
        public KickUserCommand(Guid targetUserId, Guid targetRoomId, Guid callingUserId)
        {
            if (targetUserId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "targetUserId");

            if (targetRoomId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "targetRoomId");

            if (callingUserId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "callingUserId");

            TargetUserId = targetUserId;
            TargetRoomId = targetRoomId;
            CallingUserId = callingUserId;
        }

        public Guid TargetUserId { get; private set; }

        public Guid TargetRoomId { get; private set; }

        public Guid CallingUserId { get; private set; }
    }
}