using System;
using System.Linq;
using System.Threading.Tasks;
using ChatterBox.Core.Persistence;
using ChatterBox.Domain.Aggregates.RoomAggregate;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.Domain.Extensions;
using ChatterBox.MessageContracts.Commands;
using Nimbus.Extensions;

namespace ChatterBox.ChatServer.Handlers
{
    public class AddOwnerCommandHandler : ScopedUserCommandHandler<AddOwnerCommand>
    {
        public AddOwnerCommandHandler(
            Func<IUnitOfWork> unitOfWork) 
            : base(unitOfWork)
        {
        }

        public override async Task Execute(IUnitOfWork context, User callingUser, AddOwnerCommand command)
        {
            var roomRepository = context.Repository<Room>();
            var userRepository = context.Repository<User>();

            var targetUser = userRepository.VerifyUser(command.TargetUserId);
            var room = roomRepository.VerifyRoom(command.RoomId);

            EnsureOwnerOrAdmin(callingUser, room);

            room.AddOwner(command.TargetUserId);

            if (room.Private)
            {
                if (room.Allowed.Contains(targetUser.Id) == false)
                {
                    room.AllowUser(targetUser.Id);
                }
            }

            context.Complete();
        }

        private void EnsureOwnerOrAdmin(User user, Room room)
        {
            if (!room.Owners.Contains(user.Id) && user.IsAdmin)
            {
                throw new Exception("You are not an owner of {0}.".FormatWith(room.Name));
            }
        }
    }
}