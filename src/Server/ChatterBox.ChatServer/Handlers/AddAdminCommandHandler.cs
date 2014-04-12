using System;
using System.Threading.Tasks;
using ChatterBox.Core.Persistence;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.MessageContracts.Commands;
using Nimbus.Extensions;

namespace ChatterBox.ChatServer.Handlers
{
    public class AddAdminCommandHandler : ScopedUserCommandHandler<AddAdminCommand>
    {
        public AddAdminCommandHandler(Func<IUnitOfWork> unitOfWork)
            : base(unitOfWork)
        {
        }

        public override async Task Execute(IUnitOfWork context, User callingUser, AddAdminCommand command)
        {
            EnsureAdmin(callingUser);
            
            var repository = context.Repository<User>();

            var targetUser = repository.GetById(command.TargetUserId);

            if (targetUser.IsAdmin)
            {
                throw new Exception("{0} is already an admin.".FormatWith(targetUser.Name));
            }

            targetUser.ChangeUserRole(UserRole.Admin);

            context.Complete();
        }

        private void EnsureAdmin(User user)
        {
            if (!user.IsAdmin)
            {
                throw new Exception("You are not an admin.");
            }
        }
    }
}