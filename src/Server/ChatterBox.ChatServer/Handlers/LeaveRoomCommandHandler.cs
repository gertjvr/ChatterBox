using System;
using System.Threading.Tasks;
using ChatterBox.Core.Persistence;
using ChatterBox.MessageContracts.Commands;

namespace ChatterBox.ChatServer.Handlers
{
    public class LeaveRoomCommandHandler : ScopedCommandHandler<LeaveRoomCommand>
    {
        public LeaveRoomCommandHandler(Func<IUnitOfWork> unitOfWork) : base(unitOfWork)
        {
        }

        public override Task Execute(IUnitOfWork context, LeaveRoomCommand command)
        {
            throw new NotImplementedException();
        }
    }
}