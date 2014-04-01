using System;
using System.Threading.Tasks;
using ChatterBox.MessageContracts.Owners.Commands;
using Nimbus.Handlers;

namespace ChatterBox.ChatServer.Handlers.Owners
{
    public class AddOwnerCommandHandler : IHandleCommand<AddOwnerCommand>
    {
        public Task Handle(AddOwnerCommand busCommand)
        {
            throw new NotImplementedException();
        }
    }
}