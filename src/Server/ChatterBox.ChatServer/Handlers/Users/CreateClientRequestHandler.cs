using System;
using System.Threading.Tasks;
using ChatterBox.Core.Infrastructure;
using ChatterBox.Domain.Aggregates.ClientAggregate;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.Domain.Extensions;
using ChatterBox.MessageContracts.Users.Requests;
using Nimbus.Handlers;

namespace ChatterBox.ChatServer.Handlers.Users
{
    public class CreateClientRequestHandler : IHandleRequest<CreateClientRequest, CreateClientResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Client> _clientRepository; 
        private readonly IClock _clock;

        public CreateClientRequestHandler(
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

        public async Task<CreateClientResponse> Handle(CreateClientRequest request)
        {
            if (request == null) 
                throw new ArgumentNullException("request");

            try
            {
                var callingUser = _userRepository.VerifyUser(request.CallingUserId);

                var client = _clientRepository.GetById(request.ClientId);
                if (client != null)
                {
                    return new CreateClientResponse(client.Id);
                }

                client = new Client(request.ClientId, callingUser.Id, request.UserAgent, _clock.UtcNow);

                _clientRepository.Add(client);

                _unitOfWork.Complete();

                return new CreateClientResponse(client.Id);
            }
            catch
            {
                _unitOfWork.Abandon();
                throw;
            }
        }
    }
}