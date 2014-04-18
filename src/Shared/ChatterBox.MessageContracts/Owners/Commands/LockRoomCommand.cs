using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Owners.Commands
{
    public class LockRoomCommand : IBusCommand
    {
        protected LockRoomCommand()
        {   
        }
        
        public LockRoomCommand(Guid targetRoomId)
        {
            if (targetRoomId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "targetRoomId");

            TargetRoomId = targetRoomId;
        }

        public Guid TargetRoomId { get; private set; }
    }
}