using System;
using System.Threading.Tasks;
using ChatterBox.MessageContracts.Commands;
using Nimbus.Handlers;

namespace ChatterBox.ChatServer.Handlers
{
    public class CloseRoomCommandHandler : IHandleCommand<CloseRoomCommand>
    {
        public Task Handle(CloseRoomCommand busCommand)
        {
            throw new NotImplementedException();
        }
    }
}