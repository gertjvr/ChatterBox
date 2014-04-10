using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Commands
{
    public class AddAdminCommand : IBusCommand
    {
        protected AddAdminCommand()
        {   
        }

        public AddAdminCommand(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; protected set; }
    }
}