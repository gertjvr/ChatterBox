using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Rooms.Commands
{
    public class LeaveRoomCommand : IBusCommand
    {
        public Guid UserId { get; set; }

        public Guid RoomId { get; set; }
    }
}