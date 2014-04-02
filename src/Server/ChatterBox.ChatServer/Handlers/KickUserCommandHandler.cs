using System;
using System.Threading.Tasks;
using ChatterBox.MessageContracts.Commands;
using Nimbus.Handlers;

namespace ChatterBox.ChatServer.Handlers
{
    public class KickUserCommandHandler : IHandleCommand<KickUserCommand>
    {
        public Task Handle(KickUserCommand busCommand)
        {
            throw new NotImplementedException();
        }
    }
}