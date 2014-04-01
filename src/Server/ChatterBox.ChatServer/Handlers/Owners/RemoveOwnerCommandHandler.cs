using System;
using System.Threading.Tasks;
using ChatterBox.MessageContracts.Owners.Commands;
using Nimbus.Handlers;

namespace ChatterBox.ChatServer.Handlers.Owners
{
    public class RemoveOwnerCommandHandler : IHandleCommand<RemoveOwnerCommand>
    {
        public Task Handle(RemoveOwnerCommand busCommand)
        {
            throw new NotImplementedException();
        }
    }
}