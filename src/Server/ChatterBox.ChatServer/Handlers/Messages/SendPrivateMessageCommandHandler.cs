using System;
using System.Threading.Tasks;
using ChatterBox.Core.Infrastructure;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.Domain.Extensions;
using ChatterBox.MessageContracts.Messages.Commands;
using Nimbus.Handlers;

namespace ChatterBox.ChatServer.Handlers.Messages
{
    public class SendPrivateMessageCommandHandler : IHandleCommand<SendPrivateMessageCommand>
    {
        private readonly IClock _clock;
        private readonly IRepository<User> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public SendPrivateMessageCommandHandler(
            IUnitOfWork unitOfWork,
            IRepository<User> repository,
            IClock clock)
        {
            if (unitOfWork == null)
                throw new ArgumentNullException("unitOfWork");

            if (repository == null)
                throw new ArgumentNullException("repository");

            if (clock == null)
                throw new ArgumentNullException("clock");

            _unitOfWork = unitOfWork;
            _repository = repository;
            _clock = clock;
        }

        public async Task Handle(SendPrivateMessageCommand command)
        {
            if (command == null)
                throw new ArgumentNullException("command");

            try
            {
                var callingUser = _repository.VerifyUser(command.CallingUserId);
                var user = _repository.VerifyUser(command.TargetUserId);

                user.ReceivePrivateMessage(command.Content, callingUser, _clock.UtcNow);

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