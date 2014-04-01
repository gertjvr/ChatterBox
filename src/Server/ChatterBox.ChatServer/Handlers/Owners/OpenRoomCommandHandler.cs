using System;
using System.Threading.Tasks;
using ChatterBox.MessageContracts.Owners.Commands;
using Nimbus.Handlers;

namespace ChatterBox.ChatServer.Handlers.Owners
{
    public class OpenRoomCommandHandler : IHandleCommand<OpenRoomCommand>
    {
        public Task Handle(OpenRoomCommand busCommand)
        {
            throw new NotImplementedException();
        }
    }
}