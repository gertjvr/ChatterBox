using System;
using System.Threading.Tasks;
using ChatterBox.Core.Infrastructure;
using ChatterBox.Core.Persistence;
using ChatterBox.Domain.Aggregates.RoomAggregate;
using ChatterBox.MessageContracts.Commands;

namespace ChatterBox.ChatServer.Handlers
{
    public class ChangeTopicCommandHandler : ScopedCommandHandler<ChangeTopicCommand>
    {
        private readonly IClock _clock;

        public ChangeTopicCommandHandler(
            Func<IUnitOfWork> unitOfWork,
            IClock clock) 
            : base(unitOfWork)
        {
            _clock = clock;
        }

        public override async Task Execute(IUnitOfWork context, ChangeTopicCommand command)
        {
            var repository = context.Repository<Room>();

            var room = repository.GetById(command.RoomId);

            room.ChangeTopic(command.NewTopic, command.UserId, _clock.UtcNow);

            context.Complete();
        }
    }
}