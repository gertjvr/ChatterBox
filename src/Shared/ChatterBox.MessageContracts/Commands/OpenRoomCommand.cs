using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Commands
{
    public class OpenRoomCommand : IBusCommand
    {
        public Guid RoomId { get; set; }
    }
}