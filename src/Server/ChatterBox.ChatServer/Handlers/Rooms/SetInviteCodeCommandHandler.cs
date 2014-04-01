using System;
using System.Threading.Tasks;
using ChatterBox.MessageContracts.Rooms.Commands;
using Nimbus.Handlers;

namespace ChatterBox.ChatServer.Handlers.Rooms
{
    public class SetInviteCodeCommandHandler : IHandleCommand<SetInviteCodeCommand>
    {
        public Task Handle(SetInviteCodeCommand busCommand)
        {
            throw new NotImplementedException();
        }
    }
}