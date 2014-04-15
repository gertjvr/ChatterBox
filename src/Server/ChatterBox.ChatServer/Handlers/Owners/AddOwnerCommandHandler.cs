using System;
using System.Linq;
using System.Threading.Tasks;
using ChatterBox.Core.Persistence;
using ChatterBox.Domain.Aggregates.RoomAggregate;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.Domain.Extensions;
using ChatterBox.MessageContracts.Commands;
using ChatterBox.MessageContracts.Events;
using Nimbus;

namespace ChatterBox.ChatServer.Handlers.Owners
{
    public class AddOwnerCommandHandler : ScopedUserCommandHandler<AddOwnerCommand>
    {
        public AddOwnerCommandHandler(Func<IUnitOfWork> unitOfWork, IBus bus) 
            : base(unitOfWork, bus)
        {
        }

        public override async Task Execute(IUnitOfWork context, User callingUser, AddOwnerCommand command)
        {
            var roomRepository = context.Repository<Room>();
            var userRepository = context.Repository<User>();

            var targetUser = userRepository.VerifyUser(command.TargetUserId);
            var targetRoom = roomRepository.VerifyRoom(command.TargetRoomId);

            targetRoom.EnsureOwnerOrAdmin(callingUser);

            targetRoom.AddOwner(targetUser.Id);

            if (targetRoom.Private)
            {
                if (targetRoom.AllowedUsers.Contains(targetUser.Id) == false)
                {
                    targetRoom.AllowUser(targetUser.Id);
                }
            }

            context.Complete();

            await _bus.Publish(new OwnerAddedEvent(targetUser.Id, targetUser.Name, targetRoom.Id, targetRoom.Name));
        }
    }
}