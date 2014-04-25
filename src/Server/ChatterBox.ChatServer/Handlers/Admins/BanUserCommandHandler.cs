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

namespace ChatterBox.ChatServer.Handlers.Admins
{
    public class BanUserCommandHandler : IHandleCommand<BanUserCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<User> _repository;
        private readonly IBus _bus;

        public BanUserCommandHandler(IUnitOfWork unitOfWork, IRepository<User> repository, IBus bus)
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

        public async Task Handle(BanUserCommand command)
        {
            if (command == null) 
                throw new ArgumentNullException("command");

            try
            {
                var callingUser = _repository.VerifyUser(command.CallingUserId);

                callingUser.EnsureAdmin();

                var targetUser = _repository.VerifyUser(command.TargetUserId);

                if (targetUser.IsAdministrator())
                {
                    throw new Exception(LanguageResources.Ban_CannotBanAdmin);
                }

                targetUser.UpdateUserRole(UserRole.Banned);

                _unitOfWork.Complete();

                await _bus.Publish(new UserRoleChangedEvent(targetUser.Id, targetUser.Name, (int)targetUser.Role));
            }
            catch
            {
                _unitOfWork.Abandon();
                throw;
            }
        }
    }
}