using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Rooms.Commands
{
    public class JoinRoomCommand : IBusCommand
    {
        protected JoinRoomCommand()
        {   
        }

        public JoinRoomCommand(Guid roomId, Guid userId)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "userId");
            
            if (roomId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "roomId");
            
            RoomId = roomId;
            UserId = userId;
        }

        public Guid RoomId { get; private set; }

        public Guid UserId { get; private set; }
    };
}