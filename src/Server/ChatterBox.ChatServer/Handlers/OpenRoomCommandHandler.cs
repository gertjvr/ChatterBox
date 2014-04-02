using System;
using System.Threading.Tasks;
using ChatterBox.MessageContracts.Commands;
using Nimbus.Handlers;

namespace ChatterBox.ChatServer.Handlers
{
    public class OpenRoomCommandHandler : IHandleCommand<OpenRoomCommand>
    {
        public Task Handle(OpenRoomCommand busCommand)
        {
            throw new NotImplementedException();
        }
    }
}