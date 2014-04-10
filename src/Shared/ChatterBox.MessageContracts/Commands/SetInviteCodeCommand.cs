using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Commands
{
    public class SetInviteCodeCommand : IBusCommand
    {
        protected SetInviteCodeCommand()
        {   
        }

        public SetInviteCodeCommand(Guid userId, Guid roomId, string inviteCode)
        {
            UserId = userId;
            RoomId = roomId;
            InviteCode = inviteCode;
        }

        public Guid UserId { get; protected set; }
        
        public Guid RoomId { get; protected set; }

        public string InviteCode { get; protected set; }
    }
}