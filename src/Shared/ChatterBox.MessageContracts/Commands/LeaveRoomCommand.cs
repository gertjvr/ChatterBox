using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Commands
{
    public class LeaveRoomCommand : IBusCommand
    {
        protected LeaveRoomCommand()
        {   
        }

        public LeaveRoomCommand(Guid targetRoomId, Guid userId)
        {
            TargetRoomId = targetRoomId;
            UserId = userId;
        }

        public Guid TargetRoomId { get; protected set; }

        public Guid UserId { get; protected set; }
    }
}