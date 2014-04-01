using System;
using System.Threading.Tasks;
using ChatterBox.MessageContracts.Rooms.Commands;
using Nimbus.Handlers;

namespace ChatterBox.ChatServer.Handlers.Rooms
{
    public class LeaveRoomCommandHandler : IHandleCommand<LeaveRoomCommand>
    {
        public Task Handle(LeaveRoomCommand busCommand)
        {
            throw new NotImplementedException();
        }
    }
}