using System;
using System.Threading.Tasks;
using ChatterBox.Core.Persistence;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.MessageContracts.Commands;

namespace ChatterBox.ChatServer.Handlers
{
    public class BanUserCommandHandler : ScopedCommandHandler<BanUserCommand>
    {
        public BanUserCommandHandler(Func<IUnitOfWork> unitOfWork) : base(unitOfWork)
        {
        }

        public override async Task Execute(IUnitOfWork context, BanUserCommand command)
        {
            var repository = context.Repository<User>();

            var user = repository.GetById(command.BanUserId);

            user.ChangeUserRole(UserRole.Banned);

            context.Complete();
        }
    }
}