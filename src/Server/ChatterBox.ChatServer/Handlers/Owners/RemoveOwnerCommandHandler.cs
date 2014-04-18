using System;
using System.Linq;
using System.Threading.Tasks;
using ChatterBox.Core.Persistence;
using ChatterBox.Domain.Aggregates.RoomAggregate;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.Domain.Extensions;
using ChatterBox.MessageContracts.Owners.Commands;
using Nimbus;
using ThirdDrawer.Extensions.StringExtensionMethods;

namespace ChatterBox.ChatServer.Handlers.Owners
{
    public class RemoveOwnerCommandHandler : ScopedUserCommandHandler<RemoveOwnerCommand>
    {
        public RemoveOwnerCommandHandler(Func<IUnitOfWork> unitOfWork, IBus bus) 
            : base(unitOfWork, bus)
        {
        }

        public override async Task Execute(IUnitOfWork context, User callingUser, RemoveOwnerCommand command)
        {
            var roomRepository = context.Repository<Room>();
            var userRepository = context.Repository<User>();
            
            var targetRoom = roomRepository.VerifyRoom(command.TargetRoomId);
            var targetUser = userRepository.VerifyUser(command.TargetUserId);
            
            EnsureCreatorOrAdmin(callingUser, targetRoom);

            EnsureOwnerOrAdmin(callingUser, targetRoom);

            if (targetRoom.Owners.Contains(targetUser.Id) == false)
            {
                throw new Exception("{0} is not an owner of {1}.".FormatWith(targetUser.Name, targetRoom.Name));
            }

            targetRoom.RemoveOwner(targetUser);

            context.Complete();

            //TODO Publish OwnerRemovedEvent
        }

        private void EnsureCreatorOrAdmin(User user, Room room)
        {
            if (user.Id != room.CreatorId && !user.IsAdmin)
            {
                throw new Exception("You are not the creator of {0}.".FormatWith(room.Name));
            }
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