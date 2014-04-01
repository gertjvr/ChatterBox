using System;
using System.Threading.Tasks;
using ChatterBox.MessageContracts.Owners.Commands;
using Nimbus.Handlers;

namespace ChatterBox.ChatServer.Handlers.Owners
{
    public class CloseRoomCommandHandler : IHandleCommand<CloseRoomCommand>
    {
        public Task Handle(CloseRoomCommand busCommand)
        {
            throw new NotImplementedException();
        }
    }
}