using System;
using System.Linq;
using System.Threading.Tasks;
using ChatterBox.Core.Persistence;
using ChatterBox.Domain.Aggregates.RoomAggregate;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.Domain.Extensions;
using ChatterBox.MessageContracts.Owners.Commands;
using Nimbus;
using Nimbus.Extensions;

namespace ChatterBox.ChatServer.Handlers.Owners
{
    public class UnallowUserCommandHandler : ScopedUserCommandHandler<UnallowUserCommand>
    {
        public UnallowUserCommandHandler(Func<IUnitOfWork> unitOfWork, IBus bus) 
            : base(unitOfWork, bus)
        {
        }

        public override async Task Execute(IUnitOfWork context, User callingUser, UnallowUserCommand command)
        {
            var userRepository = context.Repository<User>();
            var roomRepository = context.Repository<Room>();

            var targetUser = userRepository.VerifyUser(command.TargetUserId);
            var targetRoom = roomRepository.VerifyRoom(command.TargetRoomId);

            if (!targetRoom.Owners.Contains(targetUser.Id) && targetUser.IsAdmin)
            {
                throw new Exception("You are not an owner of {0}.".FormatWith(targetRoom.Name));
            }

            if (targetUser == callingUser)
            {
                throw new Exception("Why would you want to unallow yourself?");
            }

            if (targetRoom.PrivateRoom == false)
            {
                throw new Exception("{0} is not a private room.".FormatWith(targetRoom.Name));
            }

            if (targetRoom.AllowedUsers.Contains(targetUser.Id) == false)
            {
                throw new Exception("{0} isn't allowed to access {1}.".FormatWith(targetUser.Name, targetRoom.Name));
            }

            if (callingUser.IsAdmin == false && targetUser.IsAdmin)
            {
                throw new Exception("You cannot unallow an admin. Only admin can unallow admin.");
            }

            if (targetRoom.CreatorId != callingUser.Id && targetRoom.Owners.Contains(targetUser.Id) && callingUser.IsAdmin == false)
            {
                throw new Exception("Owners cannot unallow other owners. Only the room creator can unallow an owner.");
            }

            targetRoom.UnallowUser(targetUser);

            //TODO Make the user leave the room

            context.Complete();

            //TODO Publish UserUnallowedEvent
        }
    }
}