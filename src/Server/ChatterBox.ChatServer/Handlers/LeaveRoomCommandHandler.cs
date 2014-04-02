using System;
using System.Threading.Tasks;
using ChatterBox.MessageContracts.Commands;
using Nimbus.Handlers;

namespace ChatterBox.ChatServer.Handlers
{
    public class LeaveRoomCommandHandler : IHandleCommand<LeaveRoomCommand>
    {
        public Task Handle(LeaveRoomCommand busCommand)
        {
            throw new NotImplementedException();
        }
    }
}