using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Owners.Commands
{
    public class UnallowUserCommand : IBusCommand
    {
        public Guid RoomId { get; set; }

        public Guid UserId { get; set; }
    }
}