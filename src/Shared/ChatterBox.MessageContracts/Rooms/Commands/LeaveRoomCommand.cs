using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Rooms.Commands
{
    public class LeaveRoomCommand : IBusCommand
    {
        protected LeaveRoomCommand()
        {   
        }
        
        public LeaveRoomCommand(Guid targetRoomId, Guid userId)
        {
            if (targetRoomId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "targetRoomId");
            
            if (userId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "userId");

            TargetRoomId = targetRoomId;
            UserId = userId;
        }

        public Guid TargetRoomId { get; protected set; }

        public Guid UserId { get; protected set; }
    }
}