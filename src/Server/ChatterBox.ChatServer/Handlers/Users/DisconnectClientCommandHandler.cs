using System;
using System.Threading.Tasks;
using ChatterBox.MessageContracts.Users.Commands;
using Nimbus.Handlers;

namespace ChatterBox.ChatServer.Handlers.Users
{
    public class DisconnectClientCommandHandler : IHandleCommand<DisconnectClientCommand>
    {
        public Task Handle(DisconnectClientCommand busCommand)
        {
            throw new NotImplementedException();
        }
    }
}