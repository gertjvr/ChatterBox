using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Owners.Commands
{
    public class LockRoomCommand : IBusCommand
    {
        public Guid RoomId { get; set; }
    }
}