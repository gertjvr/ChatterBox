using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Admins.Commands
{
    public class BanUserCommand : IBusCommand
    {
        private Guid BanUserId { get; set; }
    }
}