using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Rooms.Commands
{
    public class JoinRoomCommand : IBusCommand
    {
        public Guid UserId { get; set; }

        public Guid RoomId { get; set; }

        public string InviteCode { get; set; }
    };
}