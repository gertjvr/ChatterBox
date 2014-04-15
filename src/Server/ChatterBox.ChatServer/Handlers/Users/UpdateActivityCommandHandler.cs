using System;
using System.Threading.Tasks;
using ChatterBox.Core.Infrastructure;
using ChatterBox.Core.Persistence;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.MessageContracts.Commands;

namespace ChatterBox.ChatServer.Handlers.Users
{
    public class UpdateActivityCommandHandler : ScopedCommandHandler<UpdateActivityCommand>
    {
        private readonly IClock _clock;

        public UpdateActivityCommandHandler(
            Func<IUnitOfWork> unitOfWork,
            IClock clock) 
            : base(unitOfWork)
        {
            _clock = clock;
        }

        public override async Task Execute(IUnitOfWork context, UpdateActivityCommand command)
        {
            var repository = context.Repository<User>();

            var user = repository.GetById(command.UserId);

            user.UpdateLastActivity(_clock.UtcNow);

            context.Complete();
        }
    }
}