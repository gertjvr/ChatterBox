using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatterBox.Core.Extensions;
using ChatterBox.Core.Infrastructure;
using ChatterBox.Core.Mapping;
using ChatterBox.Core.Services;
using ChatterBox.Domain.Aggregates.RoomAggregate;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.Domain.Extensions;
using ChatterBox.MessageContracts.Authentication.Request;
using ChatterBox.MessageContracts.Dtos;
using Nimbus.Extensions;
using Nimbus.Handlers;

namespace ChatterBox.ChatServer.Handlers.Authentication
{
    public class AuthenticateUserRequestHandler : IHandleRequest<AuthenticateUserRequest, AuthenticateUserResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Room> _roomRepository;
        private readonly IMapToNew<User, UserDto> _userMapper;
        private readonly IMapToNew<Room, RoomDto> _roomMapper;
        private readonly IClock _clock;
        private readonly ICryptoService _cryptoService;

        public AuthenticateUserRequestHandler(
            IUnitOfWork unitOfWork,
            IRepository<User> userRepository,
            IRepository<Room> roomRepository,
            IMapToNew<Room, RoomDto> roomMapper,
            IMapToNew<User, UserDto> userMapper,
            ICryptoService cryptoService,
            IClock clock)
        {
            if (unitOfWork == null) 
                throw new ArgumentNullException("unitOfWork");

            if (userRepository == null) 
                throw new ArgumentNullException("userRepository");

            if (roomRepository == null) 
                throw new ArgumentNullException("roomRepository");

            if (roomMapper == null) 
                throw new ArgumentNullException("roomMapper");

            if (userMapper == null) 
                throw new ArgumentNullException("userMapper");

            if (cryptoService == null) 
                throw new ArgumentNullException("cryptoService");

            if (clock == null) 
                throw new ArgumentNullException("clock");

            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _roomRepository = roomRepository;
            _userMapper = userMapper;
            _roomMapper = roomMapper;
            _cryptoService = cryptoService;
            _clock = clock;
        }

        public async Task<AuthenticateUserResponse> Handle(AuthenticateUserRequest request)
        {
            if (request == null) 
                throw new ArgumentNullException("request");

            try
            {
                IEnumerable<Room> allowedRooms;

                var user = _userRepository.GetByName(request.UserName);
                if (user == null)
                {
                    if (_userRepository.Query(users => users.None()))
                    {
                        allowedRooms = Enumerable.Empty<Room>();

                        var salt = _cryptoService.CreateSalt();
                        var hashedPassword = request.Password.ToSha256(salt);

                        user = new User(
                            Guid.NewGuid(),
                            "Admin",
                            request.UserName,
                            request.UserName.ToMD5(),
                            salt,
                            hashedPassword,
                            _clock.UtcNow,
                            UserRole.Admin);

                        _userRepository.Add(user);
                    }
                    else
                    {
                        throw new InvalidOperationException("Authentication Failed");  
                    }
                }
                else
                {
                    allowedRooms = _roomRepository.GetAllowedRoomsByUserId(user);

                    if (user.HashedPassword != request.Password.ToSha256(user.Salt))
                    {
                        throw new InvalidOperationException("Authentication Failed");
                    }

                    user.UpdateLastActivity(_clock.UtcNow);
                }

                var response = new AuthenticateUserResponse(
                    _userMapper.Map(user),
                    allowedRooms.Select(room => _roomMapper.Map(room)).ToArray(),
                    user.Id);

                _unitOfWork.Complete();

                return response;
            }
            catch
            {
                _unitOfWork.Abandon();
                throw;
            }
        }
    }
}