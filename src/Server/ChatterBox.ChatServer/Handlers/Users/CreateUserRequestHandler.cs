using System;
using System.Threading.Tasks;
using ChatterBox.Core.Extentions;
using ChatterBox.Core.Persistence;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.MessageContracts.Users.Requests;
using Nimbus.Handlers;

namespace ChatterBox.ChatServer.Handlers.Users
{
    public class CreateUserRequestHandler : IHandleRequest<CreateUserRequest, CreateUserResponse>
    {
        private readonly Func<IUnitOfWork> _unitOfWorkFactory;

        public CreateUserRequestHandler(Func<IUnitOfWork> unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public Task<CreateUserResponse> Handle(CreateUserRequest request)
        {
            using (var unitOfWork = _unitOfWorkFactory())
            {
                var repository = unitOfWork.Repository<User>();

                var user = new User(
                    request.UserName,
                    request.Email,
                    request.Email.ToMD5(),
                    request.Salt,
                    request.HashedPassword);

                repository.Add(user);

                unitOfWork.Complete();

                return Task.FromResult(new CreateUserResponse(user.Id));   
            }
        }
    }
}
