using System;

namespace ChatterBox.MessageContracts.Rooms.Commands
{
    public class SetInviteCodeCommand : IUserBusCommand
    {
        protected SetInviteCodeCommand()
        {   
        }
        
        public SetInviteCodeCommand(Guid roomId, string inviteCode, Guid userId)
        {
            if (roomId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "roomId");
            
            if (userId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "userId");
            
            if (inviteCode == null) 
                throw new ArgumentNullException("inviteCode");

            RoomId = roomId;
            InviteCode = inviteCode;
            UserId = userId;
        }

        public Guid RoomId { get; private set; }

        public string InviteCode { get; private set; }
        
        public Guid UserId { get; private set; }
    }
}