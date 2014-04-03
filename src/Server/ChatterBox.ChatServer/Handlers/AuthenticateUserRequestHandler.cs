using System;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Features.OwnedInstances;
using ChatterBox.Core.Extensions;
using ChatterBox.Core.Persistence;
using ChatterBox.Core.Services;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.Domain.Queries;
using ChatterBox.MessageContracts.Requests;

namespace ChatterBox.ChatServer.Handlers
{
    public class AuthenticateUserRequestHandler
        : ScopedRequestHandler<AuthenticateUserRequest, AuthenticateUserResponse>
    {
        private readonly ICryptoService _cryptoService;

        public AuthenticateUserRequestHandler(
            Func<Owned<IUnitOfWork>> unitOfWork,
            ICryptoService cryptoService)
            : base(unitOfWork)
        {
            _cryptoService = cryptoService;
        }

        public override async Task<AuthenticateUserResponse> Execute(IUnitOfWork context, AuthenticateUserRequest request)
        {
            var repository = context.Repository<User>();
            var query = repository.Query(new GetUserByNameQuery(request.UserName));
            var user = query.SingleOrDefault();

            if (user == null)
            {
                return new AuthenticateUserResponse(false, Guid.Empty);
            }

            if (user.HashedPassword != request.Password.ToSha256(user.Salt))
            {
                return new AuthenticateUserResponse(false, Guid.Empty);
            }

            EnsureSaltedPassword(user, request.Password);

            return new AuthenticateUserResponse(true, user.Id);
        }

        private void EnsureSaltedPassword(User user, string password)
        {
            if (String.IsNullOrEmpty(user.Salt))
            {
                user.ChangeSalt(_cryptoService.CreateSalt());
            }

            user.ChangePassword(password.ToSha256(user.Salt));
        }
    }
}