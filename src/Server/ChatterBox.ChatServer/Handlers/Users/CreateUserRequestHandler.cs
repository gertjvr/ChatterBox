using System;
using System.Threading.Tasks;
using ChatterBox.Core.Extentions;
using ChatterBox.Core.Persistence;
using ChatterBox.MessageContracts.Users.Requests;
using Domain.Aggregates.UserAggregate;
using Nimbus.Handlers;

namespace ChatterBox.ChatServer.Handlers.Users
{
    public class CreateUserRequestHandler : IHandleRequest<CreateUserRequest, CreateUserResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<User> _userRepository;

        public CreateUserRequestHandler(
            IUnitOfWork unitOfWork,
            IRepository<User> userRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public Task<CreateUserResponse> Handle(CreateUserRequest request)
        {
            var user = new User(request.UserName, request.Email, request.Email.ToMD5(), request.Salt, request.HashedPassword);

            _userRepository.Add(user);

            _unitOfWork.Complete();

            return Task.FromResult(new CreateUserResponse(user.Id));
        }
    }
}
