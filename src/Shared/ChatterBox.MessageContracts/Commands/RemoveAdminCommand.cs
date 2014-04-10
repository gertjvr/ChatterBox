using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Commands
{
    public class RemoveAdminCommand : IBusCommand
    {
        protected RemoveAdminCommand()
        {   
        }

        public RemoveAdminCommand(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; protected set; }
    }
}