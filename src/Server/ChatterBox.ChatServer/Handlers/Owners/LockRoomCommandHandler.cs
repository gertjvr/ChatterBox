using System;
using System.Threading.Tasks;
using ChatterBox.MessageContracts.Owners.Commands;
using Nimbus.Handlers;

namespace ChatterBox.ChatServer.Handlers.Owners
{
    public class LockRoomCommandHandler : IHandleCommand<LockRoomCommand>
    {
        public Task Handle(LockRoomCommand busCommand)
        {
            throw new NotImplementedException();
        }
    }
}