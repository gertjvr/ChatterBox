using System;
using System.Threading.Tasks;
using ChatterBox.Core.Persistence;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.MessageContracts.Commands;

namespace ChatterBox.ChatServer.Handlers
{
    public class AddAdminScopedCommandHandler : ScopedCommandHandler<AddAdminCommand>
    {
        public AddAdminScopedCommandHandler(Func<IUnitOfWork> unitOfWork)
            : base(unitOfWork)
        {
        }

        public override async Task Execute(IUnitOfWork context, AddAdminCommand command)
        {
            var targetUser = context.Repository<User>().GetById(command.UserId);

            targetUser.ChangeUserRole(UserRole.Admin);

            context.Complete();
        }
    }
}