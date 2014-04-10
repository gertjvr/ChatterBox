using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Commands
{
    public class KickUserCommand : IBusCommand
    {
        protected KickUserCommand()
        {   
        }

        public KickUserCommand(Guid targetUserId, Guid roomId, Guid userId)
        {
            TargetUserId = targetUserId;
            RoomId = roomId;
            UserId = userId;
        }

        public Guid TargetUserId { get; protected set; }

        public Guid RoomId { get; protected set; }

        public Guid UserId { get; protected set; }
    }
}