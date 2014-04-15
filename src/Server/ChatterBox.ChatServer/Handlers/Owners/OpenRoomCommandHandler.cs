using System;
using System.Threading.Tasks;
using ChatterBox.Core.Persistence;
using ChatterBox.Domain.Aggregates.RoomAggregate;
using ChatterBox.MessageContracts.Commands;

namespace ChatterBox.ChatServer.Handlers.Owners
{
    public class OpenRoomCommandHandler : ScopedCommandHandler<OpenRoomCommand>
    {
        public OpenRoomCommandHandler(Func<IUnitOfWork> unitOfWork) : base(unitOfWork)
        {
        }

        public override async Task Execute(IUnitOfWork context, OpenRoomCommand command)
        {
            var repository = context.Repository<Room>();

            var room = repository.GetById(command.RoomId);

            room.Open();

            context.Complete();
        }
    }
}