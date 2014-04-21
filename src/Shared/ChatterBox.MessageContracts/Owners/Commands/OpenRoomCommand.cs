using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Owners.Commands
{
    public class OpenRoomCommand : IBusCommand
    {
        protected OpenRoomCommand()
        {   
        }
        
        public OpenRoomCommand(Guid targetRoomId, Guid callingUserId)
        {
            if (targetRoomId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "targetRoomId");

            if (callingUserId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "callingUserId");

            TargetRoomId = targetRoomId;
            CallingUserId = callingUserId;
        }

        public Guid TargetRoomId { get; private set; }

        public Guid CallingUserId { get; private set; }
    }
}