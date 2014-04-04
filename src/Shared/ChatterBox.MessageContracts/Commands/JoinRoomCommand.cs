using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Commands
{
    public class JoinRoomCommand : IBusCommand
    {
        protected JoinRoomCommand()
        {
            
        }

        public JoinRoomCommand(Guid roomId, Guid userId)
        {
            RoomId = roomId;
            UserId = userId;
        }

        public string InviteCode { get; set; }

        public Guid RoomId { get; set; }

        public Guid UserId { get; set; }
    };
}