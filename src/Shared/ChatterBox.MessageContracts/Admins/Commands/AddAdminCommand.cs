using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Admins.Commands
{
    public class AddAdminCommand : IBusCommand
    {
        private Guid UserId { get; set; }
    }
}