using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Admins.Commands
{
    public class RemoveAdminCommand : IBusCommand
    {
        private Guid UserId { get; set; }
    }
}