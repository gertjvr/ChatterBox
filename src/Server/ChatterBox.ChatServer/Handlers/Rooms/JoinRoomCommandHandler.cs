using System;
using System.Threading.Tasks;
using ChatterBox.Core.Persistence;
using ChatterBox.Domain.Aggregates.RoomAggregate;
using ChatterBox.MessageContracts.Commands;

namespace ChatterBox.ChatServer.Handlers.Rooms
{
    public class JoinRoomCommandHandler : ScopedCommandHandler<JoinRoomCommand>
    {
        public JoinRoomCommandHandler(
            Func<IUnitOfWork> unitOfWork) 
            : base(unitOfWork)
        {
        }

        public override async Task Execute(IUnitOfWork context, JoinRoomCommand command)
        {
            var repository = context.Repository<Room>();

            var room = repository.GetById(command.RoomId);

            room.Join(command.UserId);

            context.Complete();
        }
    };
}