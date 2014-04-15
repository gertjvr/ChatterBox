using System;
using System.Threading.Tasks;
using ChatterBox.Core.Persistence;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.Domain.Extensions;
using ChatterBox.Domain.Properties;
using ChatterBox.MessageContracts.Commands;
using ChatterBox.MessageContracts.Events;
using Nimbus;
using ThirdDrawer.Extensions.StringExtensionMethods;

namespace ChatterBox.ChatServer.Handlers.Admins
{
    public class AddAdminCommandHandler : ScopedUserCommandHandler<AddAdminCommand>
    {
        public AddAdminCommandHandler(Func<IUnitOfWork> unitOfWork, IBus bus)
            : base(unitOfWork, bus)
        {   
        }

        public override async Task Execute(IUnitOfWork context, User callingUser, AddAdminCommand command)
        {
            callingUser.EnsureAdmin();
            
            var repository = context.Repository<User>();

            var targetUser = repository.GetById(command.TargetUserId);

            if (targetUser.IsAdmin)
            {
                throw new Exception(LanguageResources.UserAlreadyAdmin.FormatWith(targetUser.Name));
            }

            targetUser.UpdateUserRole(UserRole.Admin);

            context.Complete();

            await _bus.Publish(new AdminAddedEvent(targetUser.Id, targetUser.Name));
        }
    }
}