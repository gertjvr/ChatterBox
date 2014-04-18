using System;
using System.Threading.Tasks;
using Autofac.Features.OwnedInstances;
using ChatterBox.Core.Infrastructure;
using ChatterBox.Core.Persistence;
using ChatterBox.Domain.Aggregates.ClientAggregate;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.Domain.Extensions;
using ChatterBox.MessageContracts.Users.Requests;

namespace ChatterBox.ChatServer.Handlers.Users
{
    public class CreateClientRequestHandler : ScopedRequestHandler<CreateClientRequest, CreateClientResponse>
    {
        private readonly IClock _clock;

        public CreateClientRequestHandler(Func<Owned<IUnitOfWork>> unitOfWork, IClock clock)
            : base(unitOfWork)
        {
            _clock = clock;
        }

        public override async Task<CreateClientResponse> Execute(IUnitOfWork context, CreateClientRequest request)
        {
            var user = context.Repository<User>().VerifyUser(request.UserId);

            var repository = context.Repository<Client>();

            var client = repository.GetById(request.ClientId);
            if (client != null)
            {
                return new CreateClientResponse(client.Id);
            }

            client = new Client(request.ClientId, user.Id, request.UserAgent, _clock.UtcNow);

            repository.Add(client);

            context.Complete();

            return new CreateClientResponse(client.Id);
        }
    }
}