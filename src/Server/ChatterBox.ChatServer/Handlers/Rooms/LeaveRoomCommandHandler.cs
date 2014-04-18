using System;
using System.Threading.Tasks;
using ChatterBox.Core.Persistence;
using ChatterBox.Domain.Aggregates.RoomAggregate;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.MessageContracts.Rooms.Commands;

namespace ChatterBox.ChatServer.Handlers.Rooms
{
    public class LeaveRoomCommandHandler : ScopedCommandHandler<LeaveRoomCommand>
    {
        public LeaveRoomCommandHandler(Func<IUnitOfWork> unitOfWork) : base(unitOfWork)
        {
        }

        public override async Task Execute(IUnitOfWork context, LeaveRoomCommand command)
        {
            var roomRepository = context.Repository<Room>();
            var userRepository = context.Repository<User>();

            var room = roomRepository.GetById(command.TargetRoomId);
            var user = userRepository.GetById(command.UserId);

            room.Leave(user);

            context.Complete();
        }
    }
}