using System;
using System.Threading.Tasks;
using ChatterBox.MessageContracts.Owners.Commands;
using Nimbus.Handlers;

namespace ChatterBox.ChatServer.Handlers.Owners
{
    public class KickUserCommandHandler : IHandleCommand<KickUserCommand>
    {
        public Task Handle(KickUserCommand busCommand)
        {
            throw new NotImplementedException();
        }
    }
}