using System;
using System.Linq;
using System.Threading.Tasks;
using ChatterBox.ChatServer.Handlers.Authentication;
using ChatterBox.Core.Extensions;
using ChatterBox.Core.Infrastructure;
using ChatterBox.Core.Mapping;
using ChatterBox.Core.Services;
using ChatterBox.Core.Tests.Specifications;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.MessageContracts.Authentication.Request;
using ChatterBox.MessageContracts.Dtos;
using NSubstitute;
using Ploeh.AutoFixture;
using Shouldly;

namespace ChatterBox.ChatServer.Tests.Scenarios
{
    public class WhenAuthenticatingFirstUserWithUserNameAndPassword : AutoSpecificationForAsync<AuthenticateUserRequestHandler>
    {
        protected User User;

        protected AuthenticateUserRequest Request;
        protected AuthenticateUserResponse Response;

        protected override async Task<AuthenticateUserRequestHandler> Given()
        {
            var userName = Fixture.Create<string>();
            var password = Fixture.Create<string>();
            
            Request = new AuthenticateUserRequest(userName, password);

            var cryptoService = Fixture.Freeze<ICryptoService>();
            cryptoService.CreateSalt()
                .Returns(Fixture.Create<string>());

            return Fixture.Create<AuthenticateUserRequestHandler>();
        }

        protected override async Task When()
        {
            Response = await Subject.Handle(Request);
        }

        public void ShouldReturnCorrectUserId()
        {
            Response.UserId.ShouldBe(User.Id);
        }
    }
}