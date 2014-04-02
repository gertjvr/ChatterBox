using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Commands
{
    public class BanUserCommand : IBusCommand
    {
        private Guid BanUserId { get; set; }
    }
}