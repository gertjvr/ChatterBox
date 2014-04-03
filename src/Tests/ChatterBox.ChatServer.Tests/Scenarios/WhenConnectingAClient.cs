using System;
using ChatterBox.ChatServer.Handlers;
using ChatterBox.MessageContracts.Requests;
using Ploeh.AutoFixture;
using Shouldly;

namespace ChatterBox.ChatServer.Tests.Scenarios
{
    public class WhenConnectingAClient : AutoSpecFor<ConnectClientRequestHandler>
    {
        protected ConnectClientRequest Request;
        protected ConnectClientResponse Response;

        protected override ConnectClientRequestHandler Given()
        {
            Request = Fixture.Create<ConnectClientRequest>();

            return Fixture.Create<ConnectClientRequestHandler>();
        }

        protected override async void When()
        {
            Response = await Subject.Handle(Request);
        }

        [Then]
        public void ShouldReturnCorrectClientId()
        {
            Response.ClientId.ShouldNotBe(Guid.Empty);
        }
    }
}