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
    public class DisconnectClientCommandHandler : IHandleCommand<DisconnectClientCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Client> _clientRepository; 
        
        public DisconnectClientCommandHandler(
            IUnitOfWork unitOfWork, 
            IRepository<User> userRepository,
            IRepository<Client> clientRepository)
        {
            if (unitOfWork == null) 
                throw new ArgumentNullException("unitOfWork");
            
            if (userRepository == null) 
                throw new ArgumentNullException("userRepository");
            
            if (clientRepository == null) 
                throw new ArgumentNullException("clientRepository");
            
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _clientRepository = clientRepository;
        }

        public async Task Handle(DisconnectClientCommand command)
        {
            if (command == null) 
                throw new ArgumentNullException("command");

            try
            {
                var callingUser = _userRepository.VerifyUser(command.CallingUserId);
                var targetClient = _clientRepository.VerifyClient(command.ClientId);

                callingUser.DeregisterClient(targetClient);

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