using System;
using System.Threading.Tasks;
using ChatterBox.MessageContracts.Commands;
using Nimbus.Handlers;

namespace ChatterBox.ChatServer.Handlers
{
    public class SetInviteCodeCommandHandler : IHandleCommand<SetInviteCodeCommand>
    {
        public Task Handle(SetInviteCodeCommand busCommand)
        {
            throw new NotImplementedException();
        }
    }
}