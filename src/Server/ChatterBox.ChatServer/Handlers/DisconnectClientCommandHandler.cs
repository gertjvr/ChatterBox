using System;
using System.Threading.Tasks;
using ChatterBox.MessageContracts.Commands;
using Nimbus.Handlers;

namespace ChatterBox.ChatServer.Handlers
{
    public class DisconnectClientCommandHandler : IHandleCommand<DisconnectClientCommand>
    {
        public Task Handle(DisconnectClientCommand busCommand)
        {
            throw new NotImplementedException();
        }
    }
}