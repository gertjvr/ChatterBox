using System;
using System.Threading.Tasks;
using ChatterBox.Core.Extensions;
using ChatterBox.Core.Infrastructure;
using ChatterBox.Core.Services;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.MessageContracts.Users.Requests;
using Nimbus.Handlers;

namespace ChatterBox.ChatServer.Handlers.Users
{
    public class CreateUserRequestHandler : IHandleRequest<CreateUserRequest, CreateUserResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<User> _userRepository;
        private readonly ICryptoService _cryptoService;
        private readonly IClock _clock;

        public CreateUserRequestHandler(
            IUnitOfWork unitOfWork,
            IRepository<User> userRepository,
            ICryptoService cryptoService,
            IClock clock)
        {
            if (unitOfWork == null) 
                throw new ArgumentNullException("unitOfWork");
            
            if (userRepository == null) 
                throw new ArgumentNullException("userRepository");
            
            if (cryptoService == null) 
                throw new ArgumentNullException("cryptoService");
            
            if (clock == null) 
                throw new ArgumentNullException("clock");

            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _cryptoService = cryptoService;
            _clock = clock;
        }

        public async Task<CreateUserResponse> Handle(CreateUserRequest request)
        {
            if (request == null) 
                throw new ArgumentNullException("request");

            try
            {
                var salt = _cryptoService.CreateSalt();
                var hashedPassword = request.Password.ToSha256(salt);

                var user = new User(
                    Guid.NewGuid(),
                    request.UserName,
                    request.Email,
                    request.Email.ToMD5(),
                    salt,
                    hashedPassword,
                    _clock.UtcNow);

                _userRepository.Add(user);

                _unitOfWork.Complete();

                return new CreateUserResponse(user.Id);
            }
            catch
            {
                _unitOfWork.Abandon();
                throw;
            }
        }
    }
}
