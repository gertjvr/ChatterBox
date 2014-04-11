using System;
using System.Threading.Tasks;
using ChatterBox.Core.Persistence;
using ChatterBox.Domain.Aggregates.RoomAggregate;
using ChatterBox.MessageContracts.Commands;

namespace ChatterBox.ChatServer.Handlers
{
    public class LeaveRoomCommandHandler : ScopedCommandHandler<LeaveRoomCommand>
    {
        public LeaveRoomCommandHandler(Func<IUnitOfWork> unitOfWork) : base(unitOfWork)
        {
        }

        public override async Task Execute(IUnitOfWork context, LeaveRoomCommand command)
        {
            var repository = context.Repository<Room>();

            var room = repository.GetById(command.RoomId);

            room.Leave(command.UserId);

            context.Complete();
        }
    }
}