using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Commands
{
    public class LeaveRoomCommand : IBusCommand
    {
        protected LeaveRoomCommand()
        {
            
        }

        public LeaveRoomCommand(Guid roomId, Guid userId)
        {
            RoomId = roomId;
            UserId = userId;
        }

        public Guid UserId { get; set; }

        public Guid RoomId { get; set; }
    }
}