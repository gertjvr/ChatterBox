using System;
using System.Threading.Tasks;
using ChatterBox.MessageContracts.Commands;
using Nimbus.Handlers;

namespace ChatterBox.ChatServer.Handlers
{
    public class UnallowUserCommandHandler : IHandleCommand<UnallowUserCommand>
    {
        public Task Handle(UnallowUserCommand busCommand)
        {
            throw new NotImplementedException();
        }
    }
}