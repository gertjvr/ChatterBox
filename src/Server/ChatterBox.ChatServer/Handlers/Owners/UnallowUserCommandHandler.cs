using System;
using System.Threading.Tasks;
using ChatterBox.MessageContracts.Owners.Commands;
using Nimbus.Handlers;

namespace ChatterBox.ChatServer.Handlers.Owners
{
    public class UnallowUserCommandHandler : IHandleCommand<UnallowUserCommand>
    {
        public Task Handle(UnallowUserCommand busCommand)
        {
            throw new NotImplementedException();
        }
    }
}