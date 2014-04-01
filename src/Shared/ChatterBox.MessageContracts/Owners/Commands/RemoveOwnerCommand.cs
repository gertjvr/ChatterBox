using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Owners.Commands
{
    public class RemoveOwnerCommand : IBusCommand
    {
        public Guid RoomId { get; set; }

        public Guid OwnerId { get; set; }
    }
}