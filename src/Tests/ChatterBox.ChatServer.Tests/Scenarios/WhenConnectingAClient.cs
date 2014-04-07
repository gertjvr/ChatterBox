using System;
using System.Threading.Tasks;
using ChatterBox.ChatServer.Handlers;
using ChatterBox.MessageContracts.Requests;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Shouldly;
using SpecificationFor;

namespace ChatterBox.ChatServer.Tests.Scenarios
{
    [TestFixture]
    public class WhenConnectingAClient : AutoAsyncSpecFor<ConnectClientRequestHandler>
    {
        protected ConnectClientRequest Request;
        protected ConnectClientResponse Response;

        protected override async Task<ConnectClientRequestHandler> Given()
        {
            Request = Fixture.Create<ConnectClientRequest>();

            return Fixture.Create<ConnectClientRequestHandler>();
        }

        protected override async Task When()
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