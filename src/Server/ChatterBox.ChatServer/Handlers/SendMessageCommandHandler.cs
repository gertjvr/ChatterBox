using System;
using System.Threading.Tasks;
using ChatterBox.Core.Persistence;
using ChatterBox.Domain.Aggregates.MessageAggregate;
using ChatterBox.MessageContracts.Commands;

namespace ChatterBox.ChatServer.Handlers
{
    public class SendMessageCommandHandler : ScopedCommandHandler<SendMessageCommand>
    {
        public SendMessageCommandHandler(Func<IUnitOfWork> unitOfWork) : base(unitOfWork)
        {
        }

        public override async Task Execute(IUnitOfWork context, SendMessageCommand command)
        {
            var repository = context.Repository<Message>();

            var message = new Message(command.RoomId, command.UserId, command.Content, command.CreatedAt);

            repository.Add(message);

            context.Complete();
        }
    }
}