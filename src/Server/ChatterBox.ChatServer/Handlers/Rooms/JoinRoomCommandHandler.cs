using System;
using System.Threading.Tasks;
using ChatterBox.Core.Persistence;
using ChatterBox.Domain.Aggregates.RoomAggregate;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.MessageContracts.Rooms.Commands;

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
            var roomRepository = context.Repository<Room>();
            var userRepository = context.Repository<User>();

            var room = roomRepository.GetById(command.RoomId);
            var user = userRepository.GetById(command.UserId);

            room.Join(user);

            context.Complete();
        }
    };
}