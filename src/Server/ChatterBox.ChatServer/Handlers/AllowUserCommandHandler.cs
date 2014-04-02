using System;
using System.Threading.Tasks;
using ChatterBox.MessageContracts.Commands;
using Nimbus.Handlers;

namespace ChatterBox.ChatServer.Handlers
{
    public class AllowUserCommandHandler : IHandleCommand<AllowUserCommand>
    {
        public Task Handle(AllowUserCommand busCommand)
        {
            throw new NotImplementedException();
        }
    }
}