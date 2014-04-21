using System;
using System.Threading.Tasks;
using ChatterBox.Core.Infrastructure;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.Domain.Extensions;
using ChatterBox.Domain.Properties;
using ChatterBox.MessageContracts.Admins.Commands;
using ChatterBox.MessageContracts.Admins.Events;
using Nimbus;
using Nimbus.Handlers;
using ThirdDrawer.Extensions.StringExtensionMethods;

namespace ChatterBox.ChatServer.Handlers.Admins
{
    public class AddAdminCommandHandler : IHandleCommand<AddAdminCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<User> _repository;
        private readonly IBus _bus;

        public AddAdminCommandHandler(IUnitOfWork unitOfWork, IRepository<User> repository, IBus bus)
        {
            if (unitOfWork == null) 
                throw new ArgumentNullException("unitOfWork");

            if (repository == null) 
                throw new ArgumentNullException("repository");

            if (bus == null) 
                throw new ArgumentNullException("bus");

            _unitOfWork = unitOfWork;
            _repository = repository;
            _bus = bus;
        }

        public async Task Handle(AddAdminCommand command)
        {
            if (command == null) 
                throw new ArgumentNullException("command");

            try
            {
                var callingUser = _repository.GetById(command.CallingUserId);

                callingUser.EnsureAdmin();

                var targetUser = _repository.GetById(command.TargetUserId);

                if (targetUser.IsAdmin)
                {
                    throw new Exception(LanguageResources.UserAlreadyAdmin.FormatWith(targetUser.Name));
                }

                targetUser.UpdateUserRole(UserRole.Admin);

                _unitOfWork.Complete();

                await _bus.Publish(new AdminAddedEvent(targetUser.Id, targetUser.Name));
            }
            catch
            {
                _unitOfWork.Abandon();
                throw;
            }
        }
    }
}