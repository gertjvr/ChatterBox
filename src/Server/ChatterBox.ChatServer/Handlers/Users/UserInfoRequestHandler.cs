using System;
using System.Threading.Tasks;
using ChatterBox.Core.Infrastructure;
using ChatterBox.Core.Mapping;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.Domain.Extensions;
using ChatterBox.MessageContracts.Dtos;
using ChatterBox.MessageContracts.Users.Requests;
using Nimbus.Handlers;

namespace ChatterBox.ChatServer.Handlers.Users
{
    public class UserInfoRequestHandler : IHandleRequest<UserInfoRequest, UserInfoResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<User> _userRepository;
        private readonly IMapToNew<User, UserDto> _userMapper;

        public UserInfoRequestHandler(
            IUnitOfWork unitOfWork,
            IRepository<User> userRepository,
            IMapToNew<User, UserDto> userMapper)
        {
            if (unitOfWork == null) 
                throw new ArgumentNullException("unitOfWork");

            if (userRepository == null) 
                throw new ArgumentNullException("userRepository");

            if (userMapper == null) 
                throw new ArgumentNullException("userMapper");

            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _userMapper = userMapper;
        }

        public async Task<UserInfoResponse> Handle(UserInfoRequest request)
        {
            if (request == null) 
                throw new ArgumentNullException("request");

            try
            {
                var user = _userRepository.VerifyUser(request.TargetUserId);

                _unitOfWork.Complete();

                return new UserInfoResponse(_userMapper.Map(user));
            }
            catch
            {
                _unitOfWork.Abandon();
                throw;
            }
        }
    }
}