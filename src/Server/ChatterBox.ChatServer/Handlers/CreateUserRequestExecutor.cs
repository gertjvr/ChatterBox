using System;
using System.Threading.Tasks;
using Autofac.Features.OwnedInstances;
using ChatterBox.Core.Extensions;
using ChatterBox.Core.Persistence;
using ChatterBox.Core.Services;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.MessageContracts.Requests;

namespace ChatterBox.ChatServer.Handlers
{
    public class CreateUserRequestHandler : ScopedRequestHandler<CreateUserRequest, CreateUserResponse>
    {
        private readonly ICryptoService _cryptoService;

        public CreateUserRequestHandler(
            ICryptoService cryptoService,
            Func<Owned<IUnitOfWork>> unitOfWork)
            : base(unitOfWork)
        {
            _cryptoService = cryptoService;
        }

        public override async Task<CreateUserResponse> Execute(IUnitOfWork context, CreateUserRequest request)
        {
            var repository = context.Repository<User>();

            var salt = _cryptoService.CreateSalt();
            var hashedPassword = request.Password.ToSha256(salt);

            var user = new User(
                request.UserName,
                request.Email,
                request.Email.ToMD5(),
                salt,
                hashedPassword);

            repository.Add(user);

            context.Complete();

            return new CreateUserResponse(user.Id);
        }
    }
}
