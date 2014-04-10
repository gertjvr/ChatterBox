using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Commands
{
    public class OpenRoomCommand : IBusCommand
    {
        protected OpenRoomCommand()
        {   
        }

        public OpenRoomCommand(Guid roomId)
        {
            RoomId = roomId;
        }

        public Guid RoomId { get; protected set; }
    }
}