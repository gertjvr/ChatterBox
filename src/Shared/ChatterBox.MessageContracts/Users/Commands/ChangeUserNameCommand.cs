using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Users.Commands
{
    public class ChangeUserNameCommand : IBusCommand
    {
        protected ChangeUserNameCommand()
        {   
        }
        
        public ChangeUserNameCommand(Guid targetUserId, string newUserName, Guid callingUserId)
        {
            if (targetUserId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "targetUserId");
            
            if (newUserName == null) 
                throw new ArgumentNullException("newUserName");

            if (callingUserId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "callingUserId");

            TargetUserId = targetUserId;
            NewUserName = newUserName;
            CallingUserId = callingUserId;
        }

        public Guid TargetUserId { get; private set; }

        public string NewUserName { get; private set; }

        public Guid CallingUserId { get; private set; }
    }
}