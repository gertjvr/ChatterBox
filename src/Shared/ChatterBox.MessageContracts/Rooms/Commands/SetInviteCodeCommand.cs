using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Rooms.Commands
{
    public class SetInviteCodeCommand : IBusCommand
    {
        protected SetInviteCodeCommand()
        {   
        }
        
        public SetInviteCodeCommand(Guid targetRoomId, string inviteCode, Guid callingUserId)
        {
            if (targetRoomId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "targetRoomId");
            
            if (callingUserId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "callingUserId");
            
            if (inviteCode == null) 
                throw new ArgumentNullException("inviteCode");

            TargetRoomId = targetRoomId;
            InviteCode = inviteCode;
            CallingUserId = callingUserId;
        }

        public Guid TargetRoomId { get; private set; }

        public string InviteCode { get; private set; }
        
        public Guid CallingUserId { get; private set; }
    }
}