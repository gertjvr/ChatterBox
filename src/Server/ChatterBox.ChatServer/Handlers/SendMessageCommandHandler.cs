using System;
using System.Threading.Tasks;
using ChatterBox.Core.Infrastructure;
using ChatterBox.Core.Persistence;
using ChatterBox.Domain.Aggregates.MessageAggregate;
using ChatterBox.MessageContracts.Commands;

namespace ChatterBox.ChatServer.Handlers
{
    public class SendMessageCommandHandler : ScopedCommandHandler<SendMessageCommand>
    {
        private readonly IClock _clock;

        public SendMessageCommandHandler(
            Func<IUnitOfWork> unitOfWork,
            IClock clock) 
            : base(unitOfWork)
        {
            _clock = clock;
        }

        public override async Task Execute(IUnitOfWork context, SendMessageCommand command)
        {
            var repository = context.Repository<Message>();

            var message = new Message(command.RoomId, command.UserId, command.Content, _clock.UtcNow);

            repository.Add(message);

            context.Complete();
        }
    }
}