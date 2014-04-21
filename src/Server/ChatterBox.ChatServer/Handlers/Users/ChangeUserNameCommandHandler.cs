using System;
using System.Linq;
using System.Threading.Tasks;
using ChatterBox.Core.Infrastructure;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.Domain.Extensions;
using ChatterBox.Domain.Properties;
using ChatterBox.MessageContracts.Users.Commands;
using Nimbus.Handlers;
using ThirdDrawer.Extensions.StringExtensionMethods;

namespace ChatterBox.ChatServer.Handlers.Users
{
    public class ChangeUserNameCommandHandler : IHandleCommand<ChangeUserNameCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<User> _userRepository;

        public ChangeUserNameCommandHandler(
            IUnitOfWork unitOfWork, 
            IRepository<User> userRepository)
        {
            if (unitOfWork == null) 
                throw new ArgumentNullException("unitOfWork");

            if (userRepository == null) 
                throw new ArgumentNullException("userRepository");

            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public async Task Handle(ChangeUserNameCommand command)
        {
            if (command == null) 
                throw new ArgumentNullException("command");

            try
            {
                EnsureUserNameIsAvailible(command.NewUserName);

                var user = _userRepository.GetById(command.TargetUserId);
                user.UpdateUserName(command.NewUserName);

                _unitOfWork.Complete();
            }
            catch
            {
                _unitOfWork.Abandon();
                throw;
            }
        }

        private void EnsureUserNameIsAvailible(string userName)
        {
            if (_userRepository.GetByName(userName) != null)
                throw new InvalidOperationException(LanguageResources.UserNameTaken.FormatWith(userName));
        }
    }
 }