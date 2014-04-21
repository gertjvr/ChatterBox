using System;
using System.Threading.Tasks;
using ChatterBox.ChatServer.Handlers.Users;
using ChatterBox.Core.Infrastructure;
using ChatterBox.Core.Tests.Specifications;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.MessageContracts.Users.Requests;
using NSubstitute;
using Ploeh.AutoFixture;
using Shouldly;

namespace ChatterBox.ChatServer.Tests.Scenarios
{
    public class WhenConnectingAClient : AutoSpecificationForAsync<CreateClientRequestHandler>
    {
        protected User CallingUser;

        protected CreateClientRequest Request;
        protected CreateClientResponse Response;

        protected override async Task<CreateClientRequestHandler> Given()
        {
            CallingUser = Fixture.Create<User>();
            
            Request = new CreateClientRequest(Fixture.Create<Guid>(), Fixture.Create<string>(), CallingUser.Id);

            var repository = Fixture.Freeze<IRepository<User>>();
            repository.GetById(Arg.Is(CallingUser.Id))
                .Returns(CallingUser);

            return Fixture.Create<CreateClientRequestHandler>();
        }

        protected override async Task When()
        {
            Response = await Subject.Handle(Request);
        }

        public void ShouldReturnCorrectClientId()
        {
            Response.ClientId.ShouldNotBe(Guid.Empty);
        }
    }
}