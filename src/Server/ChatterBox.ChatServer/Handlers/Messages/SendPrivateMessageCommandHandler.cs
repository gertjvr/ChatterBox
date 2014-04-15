using System;
using System.Threading.Tasks;
using ChatterBox.Core.Infrastructure;
using ChatterBox.Core.Persistence;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.MessageContracts.Commands;

namespace ChatterBox.ChatServer.Handlers.Messages
{
    public class SendPrivateMessageCommandHandler : ScopedCommandHandler<SendPrivateMessageCommand>
    {
        private readonly IClock _clock;

        public SendPrivateMessageCommandHandler(
            Func<IUnitOfWork> unitOfWork,
            IClock clock) 
            : base(unitOfWork)
        {
            _clock = clock;
        }

        public override async Task Execute(IUnitOfWork context, SendPrivateMessageCommand command)
        {
            var repository = context.Repository<User>();

            var user = repository.GetById(command.TargetUserId);

            user.ReceivePrivateMessage(command.Content, command.UserId, _clock.UtcNow);

            context.Complete();
        }
    }
}