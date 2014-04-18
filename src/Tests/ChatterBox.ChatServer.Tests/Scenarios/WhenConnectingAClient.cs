using System;
using System.Threading.Tasks;
using ChatterBox.ChatServer.Handlers.Users;
using ChatterBox.Core.Tests;
using ChatterBox.Core.Tests.Specifications;
using ChatterBox.MessageContracts.Users.Requests;
using Ploeh.AutoFixture;
using Shouldly;

namespace ChatterBox.ChatServer.Tests.Scenarios
{
    public class WhenConnectingAClient : AutoSpecificationForAsync<CreateClientRequestHandler>
    {
        protected CreateClientRequest Request;
        protected CreateClientResponse Response;

        protected override async Task<CreateClientRequestHandler> Given()
        {
            Request = Fixture.Create<CreateClientRequest>();

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