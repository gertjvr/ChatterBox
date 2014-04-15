using System;
using System.Threading.Tasks;
using ChatterBox.Core.Persistence;
using ChatterBox.Domain.Aggregates.RoomAggregate;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.Domain.Extensions;
using ChatterBox.Domain.Properties;
using ChatterBox.MessageContracts.Commands;
using Nimbus;

namespace ChatterBox.ChatServer.Handlers.Rooms
{
    public class SetInviteCodeCommandHandler : ScopedUserCommandHandler<SetInviteCodeCommand>
    {
        public SetInviteCodeCommandHandler(Func<IUnitOfWork> unitOfWork, IBus bus) 
            : base(unitOfWork, bus)
        {
        }

        public override async Task Execute(IUnitOfWork context, User callingUser, SetInviteCodeCommand command)
        {
            var room = context.Repository<Room>().VerifyRoom(command.RoomId);

            room.EnsureOwnerOrAdmin(callingUser);

            if (!room.Private)
            {
                throw new Exception(LanguageResources.InviteCode_PrivateRoomRequired);
            }

            room.SetInviteCode(command.InviteCode);
            context.Complete();
        }
    }
}