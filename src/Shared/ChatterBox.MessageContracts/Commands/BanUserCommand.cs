using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Commands
{
    public class BanUserCommand : IBusCommand
    {
        protected BanUserCommand()
        {
        }

        public BanUserCommand(Guid banUserId)
        {
            BanUserId = banUserId;
        }

        public Guid BanUserId { get; protected set; }
    }
}