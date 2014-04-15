using System;
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
    public class ChangeTopicCommandHandler : ScopedUserCommandHandler<ChangeTopicCommand>
    {
        public ChangeTopicCommandHandler(Func<IUnitOfWork> unitOfWork, IBus bus) 
            : base(unitOfWork, bus)
        {
        }

        public override async Task Execute(IUnitOfWork context, User callingUser, ChangeTopicCommand command)
        {
            var repository = context.Repository<Room>();

            var targetRoom = repository.VerifyRoom(command.RoomId);

            targetRoom.EnsureOpen();
            targetRoom.EnsureOwnerOrAdmin(callingUser);

            targetRoom.ChangeTopic(command.NewTopic);

            context.Complete();

            await _bus.Publish(new RoomTopicChangedEvent(targetRoom.Id, targetRoom.Name, targetRoom.Topic));
        }
    }
}