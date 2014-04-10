using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Commands
{
    public class LockRoomCommand : IBusCommand
    {
        protected LockRoomCommand()
        {   
        }

        public LockRoomCommand(Guid roomId)
        {
            RoomId = roomId;
        }

        public Guid RoomId { get; protected set; }
    }
}