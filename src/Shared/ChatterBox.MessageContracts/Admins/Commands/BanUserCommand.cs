using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Admins.Commands
{
    public class BanUserCommand : IBusCommand
    {
        protected BanUserCommand()
        {   
        }
        
        public BanUserCommand(Guid targetUserId, Guid callingUserId)
        {
            if (targetUserId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "targetUserId");

            if (callingUserId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "callingUserId");

            TargetUserId = targetUserId;
            CallingUserId = callingUserId;
        }

        public Guid TargetUserId { get; private set; }

        public Guid CallingUserId { get; private set; }
    }
}