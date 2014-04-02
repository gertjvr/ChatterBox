using System;
using System.Threading.Tasks;
using ChatterBox.MessageContracts.Commands;
using Nimbus.Handlers;

namespace ChatterBox.ChatServer.Handlers
{
    public class LockRoomCommandHandler : IHandleCommand<LockRoomCommand>
    {
        public Task Handle(LockRoomCommand busCommand)
        {
            throw new NotImplementedException();
        }
    }
}