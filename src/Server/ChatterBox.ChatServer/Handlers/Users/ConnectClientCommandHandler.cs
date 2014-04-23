using System;
using System.Threading.Tasks;
using ChatterBox.Core.Infrastructure;
using ChatterBox.Domain.Aggregates.ClientAggregate;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.Domain.Extensions;
using ChatterBox.MessageContracts.Users.Commands;
using Nimbus.Handlers;

namespace ChatterBox.ChatServer.Handlers.Users
{
    public class ConnectClientCommandHandler : IHandleCommand<ConnectClientCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Client> _clientRepository; 
        private readonly IClock _clock;

        public ConnectClientCommandHandler(
            IUnitOfWork unitOfWork, 
            IRepository<User> userRepository,
            IRepository<Client> clientRepository, 
            IClock clock)
        {
            if (unitOfWork == null) 
                throw new ArgumentNullException("unitOfWork");
            
            if (userRepository == null) 
                throw new ArgumentNullException("userRepository");
            
            if (clientRepository == null) 
                throw new ArgumentNullException("clientRepository");
            
            if (clock == null) 
                throw new ArgumentNullException("clock");

            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _clock = clock;
            _clientRepository = clientRepository;
        }

        public async Task Handle(ConnectClientCommand command)
        {
            if (command == null) 
                throw new ArgumentNullException("command");

            try
            {
                var callingUser = _userRepository.VerifyUser(command.CallingUserId);

                var client = _clientRepository.GetById(command.ClientId);
                if (client != null)
                {
                    return;
                }

                client = new Client(command.ClientId, callingUser.Id, command.UserAgent, _clock.UtcNow);

                _clientRepository.Add(client);

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