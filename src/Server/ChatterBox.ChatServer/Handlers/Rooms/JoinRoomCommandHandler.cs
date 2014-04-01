using System;
using System.Threading.Tasks;
using ChatterBox.MessageContracts.Rooms.Commands;
using Nimbus.Handlers;

namespace ChatterBox.ChatServer.Handlers.Rooms
{
    public class JoinRoomCommandHandler : IHandleCommand<JoinRoomCommand>
    {
        public Task Handle(JoinRoomCommand busCommand)
        {
            throw new NotImplementedException();
        }
    };
}