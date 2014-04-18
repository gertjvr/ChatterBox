using System;
using System.Threading.Tasks;
using ChatterBox.Core.Persistence;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.Domain.Extensions;
using ChatterBox.Domain.Properties;
using ChatterBox.MessageContracts.Admins.Commands;
using ChatterBox.MessageContracts.Admins.Events;
using Nimbus;

namespace ChatterBox.ChatServer.Handlers.Admins
{
    public class RemoveAdminCommandHandler : ScopedUserCommandHandler<RemoveAdminCommand>
    {
        public RemoveAdminCommandHandler(Func<IUnitOfWork> unitOfWork, IBus bus) 
            : base(unitOfWork, bus)
        {
        }

        public override async Task Execute(IUnitOfWork context, User callingUser, RemoveAdminCommand command)
        {
            var targetUser = context.Repository<User>().VerifyUser(command.TargetUserId);

            callingUser.EnsureAdmin();

            if (!targetUser.IsAdmin)
            {
                throw new Exception(String.Format(LanguageResources.UserNotAdmin, targetUser.Name));
            }

            targetUser.UpdateUserRole(UserRole.User);

            context.Complete();

            await _bus.Publish(new UserRoleChangedEvent(targetUser.Id, targetUser.Name, (int)targetUser.UserRole));
        }
    }
}