using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Commands
{
    public class ChangeUserNameCommand : IBusCommand
    {
        protected ChangeUserNameCommand()
        {
        }

        public ChangeUserNameCommand(Guid userId, string newUserName)
        {
            UserId = userId;
            NewUserName = newUserName;
        }

        public Guid UserId { get; set; }

        public string NewUserName { get; set; }
    }
}