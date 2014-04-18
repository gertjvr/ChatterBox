using System;
using System.Linq;
using System.Threading.Tasks;
using ChatterBox.Core.Persistence;
using ChatterBox.Domain.Aggregates.RoomAggregate;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.Domain.Extensions;
using ChatterBox.Domain.Properties;
using ChatterBox.MessageContracts.Owners.Commands;
using ChatterBox.MessageContracts.Owners.Events;
using Nimbus;
using ThirdDrawer.Extensions.StringExtensionMethods;

namespace ChatterBox.ChatServer.Handlers.Owners
{
    public class AllowUserCommandHandler : ScopedUserCommandHandler<AllowUserCommand>
    {
        public AllowUserCommandHandler(Func<IUnitOfWork> unitOfWork, IBus bus) 
            : base(unitOfWork, bus)
        {
        }

        public override async Task Execute(IUnitOfWork context, User callingUser, AllowUserCommand command)
        {
            var roomRepository = context.Repository<Room>();
            var userRepository = context.Repository<User>();
 
            var targetRoom = roomRepository.VerifyRoom(command.TargetRoomId);
            var targetUser = userRepository.VerifyUser(command.TargetUserId);

            targetRoom.EnsureOwnerOrAdmin(callingUser);

            if (targetRoom.PrivateRoom == false) 
            {
                throw new Exception(LanguageResources.RoomNotPrivate.FormatWith(targetRoom.Name));
            }

            if (targetRoom.AllowedUsers.Contains(targetUser.Id))
            {
                throw new Exception(LanguageResources.RoomUserAlreadyAllowed.FormatWith(targetUser.Name, targetRoom.Name));
            }

            targetRoom.AllowUser(targetUser);

            context.Complete();

            await _bus.Publish(new UserAllowedEvent(targetUser.Id, targetUser.Name, targetRoom.Id, targetRoom.Name));
        }
    }
}