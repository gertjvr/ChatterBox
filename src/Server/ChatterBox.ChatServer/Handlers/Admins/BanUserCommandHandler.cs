using System;
using System.Threading.Tasks;
using ChatterBox.Core.Persistence;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.Domain.Extensions;
using ChatterBox.Domain.Properties;
using ChatterBox.MessageContracts.Commands;
using ChatterBox.MessageContracts.Events;
using Nimbus;

namespace ChatterBox.ChatServer.Handlers.Admins
{
    public class BanUserCommandHandler : ScopedUserCommandHandler<BanUserCommand>
    {
        public BanUserCommandHandler(Func<IUnitOfWork> unitOfWork, IBus bus)
            : base(unitOfWork, bus)
        {
        }

        public override async Task Execute(IUnitOfWork context, User callingUser, BanUserCommand command)
        {
            callingUser.EnsureAdmin();

            var repository = context.Repository<User>();

            var targetUser = repository.VerifyUser(command.TargetUserId);

            if (targetUser.IsAdmin)
            {
                throw new Exception(LanguageResources.Ban_CannotBanAdmin);
            }

            targetUser.UpdateUserRole(UserRole.Banned);

            context.Complete();

            await _bus.Publish(new UserRoleChangedEvent(targetUser.Id, targetUser.Name, (int)targetUser.UserRole));
        }
    }
}