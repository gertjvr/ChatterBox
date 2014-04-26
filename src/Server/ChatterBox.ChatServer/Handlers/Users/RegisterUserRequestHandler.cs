using System;
using System.Linq;
using System.Threading.Tasks;
using ChatterBox.Core.Extensions;
using ChatterBox.Core.Infrastructure;
using ChatterBox.Core.Mapping;
using ChatterBox.Core.Services;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.MessageContracts.Dtos;
using ChatterBox.MessageContracts.Users.Requests;
using Nimbus.Handlers;

namespace ChatterBox.ChatServer.Handlers.Users
{
    public class RegisterUserRequestHandler : IHandleRequest<RegisterUserRequest, RegisterUserResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<User> _userRepository;
        private readonly IMapToNew<User, UserDto> _userMapper;
        private readonly ICryptoService _cryptoService;
        private readonly IClock _clock;

        public RegisterUserRequestHandler(
            IUnitOfWork unitOfWork,
            IRepository<User> userRepository,
            IMapToNew<User, UserDto> userMapper,
            ICryptoService cryptoService,
            IClock clock)
        {
            if (unitOfWork == null) 
                throw new ArgumentNullException("unitOfWork");
            
            if (userRepository == null) 
                throw new ArgumentNullException("userRepository");
            
            if (userMapper == null) 
                throw new ArgumentNullException("userMapper");

            if (cryptoService == null) 
                throw new ArgumentNullException("cryptoService");
            
            if (clock == null) 
                throw new ArgumentNullException("clock");

            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _userMapper = userMapper;
            _cryptoService = cryptoService;
            _clock = clock;
        }

        public async Task<RegisterUserResponse> Handle(RegisterUserRequest request)
        {
            if (request == null) 
                throw new ArgumentNullException("request");

            try
            {
                var salt = _cryptoService.CreateSalt();
                var hashedPassword = request.Password.ToSha256(salt);

                var user = new User(
                    request.UserName,
                    request.EmailAddress,
                    request.EmailAddress.ToMD5(),
                    salt,
                    hashedPassword,
                    _clock.UtcNow);

                _userRepository.Add(user);

                _unitOfWork.Complete();

                return new RegisterUserResponse(
                    _userMapper.Map(user),
                    Enumerable.Empty<RoomDto>().ToArray(),
                    user.Id);
            }
            catch
            {
                _unitOfWork.Abandon();
                throw;
            }
        }
    }
}
