using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Rooms.Commands
{
    public class JoinRoomCommand : IBusCommand
    {
        protected JoinRoomCommand()
        {   
        }

        public JoinRoomCommand(Guid targetRoomId, Guid targetUserId, Guid callingUserId)
        {
            if (targetUserId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "targetUserId");
            
            if (targetRoomId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "targetRoomId");

            if (callingUserId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "callingUserId");
            
            TargetRoomId = targetRoomId;
            TargetUserId = targetUserId;
            CallingUserId = callingUserId;
        }

        public Guid TargetRoomId { get; private set; }

        public Guid TargetUserId { get; private set; }
        
        public Guid CallingUserId { get; private set; }
    };
}