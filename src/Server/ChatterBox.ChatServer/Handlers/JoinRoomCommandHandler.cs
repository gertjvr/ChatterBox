using System;
using System.Threading.Tasks;
using ChatterBox.MessageContracts.Commands;
using Nimbus.Handlers;

namespace ChatterBox.ChatServer.Handlers
{
    public class JoinRoomCommandHandler : IHandleCommand<JoinRoomCommand>
    {
        public Task Handle(JoinRoomCommand busCommand)
        {
            throw new NotImplementedException();
        }
    };
}