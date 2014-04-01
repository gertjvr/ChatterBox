using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Owners.Commands
{
    public class OpenRoomCommand : IBusCommand
    {
        public Guid RoomId { get; set; }
    }
}