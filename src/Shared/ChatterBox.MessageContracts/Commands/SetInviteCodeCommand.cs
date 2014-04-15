using System;

namespace ChatterBox.MessageContracts.Commands
{
    public class SetInviteCodeCommand : IUserBusCommand
    {
        public SetInviteCodeCommand(Guid roomId, string inviteCode, Guid userId)
        {
            RoomId = roomId;
            InviteCode = inviteCode;
            UserId = userId;
        }

        public Guid RoomId { get; protected set; }

        public string InviteCode { get; protected set; }
        
        public Guid UserId { get; protected set; }
    }
}