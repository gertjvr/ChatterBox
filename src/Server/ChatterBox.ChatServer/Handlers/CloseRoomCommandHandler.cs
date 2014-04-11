using System;
using System.Threading.Tasks;
using ChatterBox.Core.Persistence;
using ChatterBox.Domain.Aggregates.RoomAggregate;
using ChatterBox.MessageContracts.Commands;

namespace ChatterBox.ChatServer.Handlers
{
    public class CloseRoomCommandHandler : ScopedCommandHandler<CloseRoomCommand>
    {
        public CloseRoomCommandHandler(Func<IUnitOfWork> unitOfWork) : base(unitOfWork)
        {
        }

        public override async Task Execute(IUnitOfWork context, CloseRoomCommand command)
        {
            var repository = context.Repository<Room>();

            var room = repository.GetById(command.RoomId);

            room.Close(command.UserId);

            context.Complete();
        }
    }
}