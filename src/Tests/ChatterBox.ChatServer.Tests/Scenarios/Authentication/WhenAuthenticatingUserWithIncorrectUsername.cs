using System;
using System.Threading.Tasks;
using ChatterBox.ChatServer.Handlers.Authentication;
using ChatterBox.Core.Tests.Specifications;
using ChatterBox.MessageContracts.Authentication.Request;
using Ploeh.AutoFixture;
using Shouldly;
using ThirdDrawer.Extensions.StringExtensionMethods;

namespace ChatterBox.ChatServer.Tests.Scenarios.Authentication
{
    public class WhenAuthenticatingUserWithIncorrectUsername :
        AutoSpecificationForAsync<AuthenticateUserRequestHandler>
    {
        protected AuthenticateUserRequest Request;

        protected string Username;
        protected string Password;

        protected override async Task<AuthenticateUserRequestHandler> Given()
        {
            Username = "username{0}".FormatWith(Fixture.Create<string>());
            Password = "password{0}".FormatWith(Fixture.Create<string>());

            Request = new AuthenticateUserRequest(Username, Password);

            return Fixture.Create<AuthenticateUserRequestHandler>();
        }

        protected override async Task When()
        {   
        }

        [Then]
        public void ShouldHaveCreatedAdminUser()
        {
            var e = Should.Throw<InvalidOperationException>(async () => await Subject.Handle(Request));
            e.Message.ShouldBe("Authentication Failed.");
        }
    }
}