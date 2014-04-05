using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Features.OwnedInstances;
using ChatterBox.Core.Extensions;
using ChatterBox.Core.Infrastructure;
using ChatterBox.Core.Mapping;
using ChatterBox.Core.Persistence;
using ChatterBox.Core.Services;
using ChatterBox.Domain.Aggregates.RoomAggregate;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.Domain.Queries;
using ChatterBox.MessageContracts.Dtos;
using ChatterBox.MessageContracts.Requests;

namespace ChatterBox.ChatServer.Handlers
{
    public class AuthenticateUserRequestHandler
        : ScopedRequestHandler<AuthenticateUserRequest, AuthenticateUserResponse>
    {
        private readonly ICryptoService _cryptoService;
        private readonly IMapToNew<User, UserDto> _userMapper;
        private readonly IMapToNew<Room, RoomDto> _roomMapper;

        public AuthenticateUserRequestHandler(
            Func<Owned<IUnitOfWork>> unitOfWork,
            ICryptoService cryptoService,
            IMapToNew<Room, RoomDto> roomMapper,
            IMapToNew<User, UserDto> userMapper)
            : base(unitOfWork)
        {
            _cryptoService = cryptoService;
            _userMapper = userMapper;
            _roomMapper = roomMapper;
        }

        public override async Task<AuthenticateUserResponse> Execute(IUnitOfWork context, AuthenticateUserRequest request)
        {
            var userRepository = context.Repository<User>();
            var roomRepository = context.Repository<Room>();

            User user;
            IEnumerable<Room> rooms; 
            if (userRepository.Query(new EnsureUsersExistsQuery()))
            {
                var salt = _cryptoService.CreateSalt();
                var hashedPassword = request.Password.ToSha256(salt);

                user = new User(
                    request.UserName,
                    string.Empty,
                    string.Empty.ToMD5(),
                    salt,
                    hashedPassword, 
                    UserRole.Admin);

                rooms = Enumerable.Empty<Room>();

                userRepository.Add(user);
            }
            else
            {
                var userId = userRepository.Query(new GetUserIdByNameQuery(request.UserName));

                user = userRepository.GetById(userId);

                if (user == null)
                {
                    return AuthenticateUserResponse.Failed();
                }

                rooms = roomRepository
                    .Query(new GetRoomsForUserIdQuery(user.Id))
                    .ToList();

                if (user.HashedPassword != request.Password.ToSha256(user.Salt))
                {
                    return AuthenticateUserResponse.Failed();
                }

                user.UpdateLastActivity(DateTimeHelper.UtcNow);
            }

            context.Complete();

            return new AuthenticateUserResponse(
                _userMapper.Map(user), 
                rooms.Select(room => _roomMapper.Map(room)).ToArray(), 
                Guid.NewGuid(),
                user.Id);
        }
    }
}