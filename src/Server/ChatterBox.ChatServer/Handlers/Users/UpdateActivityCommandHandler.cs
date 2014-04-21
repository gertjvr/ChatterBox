using System;
using System.Threading.Tasks;
using ChatterBox.Core.Infrastructure;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.Domain.Extensions;
using ChatterBox.MessageContracts.Users.Commands;
using Nimbus.Handlers;

namespace ChatterBox.ChatServer.Handlers.Users
{
    public class UpdateActivityCommandHandler : IHandleCommand<UpdateActivityCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<User> _userRepository;
        private readonly IClock _clock;

        public UpdateActivityCommandHandler(
            IUnitOfWork unitOfWork,
            IRepository<User> userRepository,
            IClock clock)
        {
            if (unitOfWork == null) 
                throw new ArgumentNullException("unitOfWork");
            
            if (userRepository == null) 
                throw new ArgumentNullException("userRepository");
            
            if (clock == null) 
                throw new ArgumentNullException("clock");

            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _clock = clock;
        }

        public async Task Handle(UpdateActivityCommand command)
        {
            if (command == null) 
                throw new ArgumentNullException("command");

            try
            {
                var callingUser = _userRepository.VerifyUser(command.CallingUserId);
                var targetUser = _userRepository.VerifyUser(command.TargetUserId);

                targetUser.UpdateLastActivity(_clock.UtcNow);

                _unitOfWork.Complete();
            }
            catch
            {
                _unitOfWork.Abandon();
                throw;
            }
        }
    }
}