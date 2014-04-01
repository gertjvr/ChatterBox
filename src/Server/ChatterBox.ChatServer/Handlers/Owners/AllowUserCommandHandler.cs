using System;
using System.Threading.Tasks;
using ChatterBox.MessageContracts.Owners.Commands;
using Nimbus.Handlers;

namespace ChatterBox.ChatServer.Handlers.Owners
{
    public class AllowUserCommandHandler : IHandleCommand<AllowUserCommand>
    {
        public Task Handle(AllowUserCommand busCommand)
        {
            throw new NotImplementedException();
        }
    }
}