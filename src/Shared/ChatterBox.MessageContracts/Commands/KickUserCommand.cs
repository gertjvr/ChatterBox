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

        public Guid TargetUserId { get; set; }

        public Guid RoomId { get; set; }

        public Guid UserId { get; set; }
    }
}