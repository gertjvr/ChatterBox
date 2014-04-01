using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Owners.Commands
{
    public class CloseRoomCommand : IBusCommand
    {
        public Guid RoomId { get; set; }

        public Guid UserId { get; set; }
    }
}