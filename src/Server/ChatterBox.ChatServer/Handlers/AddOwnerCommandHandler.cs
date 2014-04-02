using System;
using System.Threading.Tasks;
using ChatterBox.MessageContracts.Commands;
using Nimbus.Handlers;

namespace ChatterBox.ChatServer.Handlers
{
    public class AddOwnerCommandHandler : IHandleCommand<AddOwnerCommand>
    {
        public Task Handle(AddOwnerCommand busCommand)
        {
            throw new NotImplementedException();
        }
    }
}