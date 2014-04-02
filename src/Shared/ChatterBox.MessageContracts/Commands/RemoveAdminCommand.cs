using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Commands
{
    public class RemoveAdminCommand : IBusCommand
    {
        private Guid UserId { get; set; }
    }
}