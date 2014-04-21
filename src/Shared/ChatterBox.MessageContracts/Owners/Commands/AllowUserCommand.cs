using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Owners.Commands
{
    public class AllowUserCommand : IBusCommand
    {
        protected AllowUserCommand()
        {   
        }

        public AllowUserCommand(Guid targetRoomId, Guid targetUserId, Guid callingUserId)
        {
            if (targetRoomId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "targetRoomId");

            if (targetUserId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "targetUserId");

            if (callingUserId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "callingUserId");

            TargetRoomId = targetRoomId;
            TargetUserId = targetUserId;
            CallingUserId = callingUserId;
        }

        public Guid TargetRoomId { get; private set; }

        public Guid TargetUserId { get; private set; }

        public Guid CallingUserId { get; private set; }
    }
}