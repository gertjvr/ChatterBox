using System;
using System.Threading.Tasks;
using ChatterBox.Core.Persistence;
using ChatterBox.Domain.Aggregates.RoomAggregate;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.Domain.Extensions;
using ChatterBox.MessageContracts.Commands;
using ChatterBox.MessageContracts.Events;
using Nimbus;
using ThirdDrawer.Extensions.StringExtensionMethods;

namespace ChatterBox.ChatServer.Handlers.Owners
{
    public class CloseRoomCommandHandler : ScopedUserCommandHandler<CloseRoomCommand>
    {
        public CloseRoomCommandHandler(Func<IUnitOfWork> unitOfWork, IBus bus) 
            : base(unitOfWork, bus)
        {
        }

        public override async Task Execute(IUnitOfWork context, User callingUser, CloseRoomCommand command)
        {
            var repository = context.Repository<Room>();

            var targetRoom = repository.VerifyRoom(command.TargetRoomId);

            targetRoom.EnsureOwnerOrAdmin(callingUser);

            if (targetRoom.Closed)
            {
                throw new Exception("{0} is already closed.".FormatWith(targetRoom.Name));
            }

            targetRoom.Close();

            context.Complete();

            await _bus.Publish(new RoomClosedEvent(targetRoom.Id, targetRoom.Name));
        }
    }
}