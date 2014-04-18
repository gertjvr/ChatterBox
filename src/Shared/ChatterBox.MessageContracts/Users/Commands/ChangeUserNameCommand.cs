using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Users.Commands
{
    public class ChangeUserNameCommand : IBusCommand
    {
        protected ChangeUserNameCommand()
        {   
        }
        
        public ChangeUserNameCommand(Guid userId, string newUserName)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "userId");
            
            if (newUserName == null) 
                throw new ArgumentNullException("newUserName");

            UserId = userId;
            NewUserName = newUserName;
        }

        public Guid UserId { get; private set; }

        public string NewUserName { get; private set; }
    }
}