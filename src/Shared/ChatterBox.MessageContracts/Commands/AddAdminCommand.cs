using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Commands
{
    public class AddAdminCommand : IBusCommand
    {
        public Guid UserId { get; set; }
    }
}