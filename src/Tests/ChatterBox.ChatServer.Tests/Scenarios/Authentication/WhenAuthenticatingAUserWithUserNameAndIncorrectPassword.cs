using System;
using System.Linq;
using System.Threading.Tasks;
using ChatterBox.ChatServer.Handlers.Authentication;
using ChatterBox.Core.Infrastructure;
using ChatterBox.Core.Infrastructure.Queries;
using ChatterBox.Core.Mapping;
using ChatterBox.Core.Services;
using ChatterBox.Core.Tests;
using ChatterBox.Core.Tests.Specifications;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.MessageContracts.Authentication.Request;
using ChatterBox.MessageContracts.Dtos;
using NSubstitute;
using Ploeh.AutoFixture;
using Shouldly;
using ThirdDrawer.Extensions.StringExtensionMethods;

namespace ChatterBox.ChatServer.Tests.Scenarios.Authentication
{
    public class WhenAuthenticatingAUserWithUserNameAndIncorrectPassword : AutoSpecificationForAsync<AuthenticateUserRequestHandler>
    {
        protected User User;
        protected IClock Clock;

        protected IRepository<User> Repository;

        protected AuthenticateUserRequest Request;

        protected string UserName;
        protected string EmailAddress;
        protected string Salt;
        protected string Password;

        protected override async Task<AuthenticateUserRequestHandler> Given()
        {
            Clock = Fixture.Freeze<IClock>();
            Clock.UtcNow.Returns(DateTimeOffset.UtcNow);
            
            UserName = "userName{0}".FormatWith(Fixture.Create<string>());
            EmailAddress = "emailAddress{0}".FormatWith(Fixture.Create<string>());
            Salt = "salt{0}".FormatWith(Fixture.Create<string>());
            Password = "password{0}".FormatWith(Fixture.Create<string>());

            User = ObjectMother.CreateUser(UserName, EmailAddress, Salt, Password, Clock.UtcNow);

            Request = new AuthenticateUserRequest(UserName, Fixture.Create<string>());

            Repository = Fixture.Freeze<IRepository<User>>();
            Repository.Query(Arg.Any<Func<IQueryable<User>, User>>())
                .Returns(User);

            return Fixture.Create<AuthenticateUserRequestHandler>();
        }

        protected override async Task When()
        {
        }

        [Then]
        public void ShouldNotCreatedAdminUser()
        {
            var e = Should.Throw<InvalidOperationException>(async () => await Subject.Handle(Request));
            e.Message.ShouldBe("Authentication Failed.");
        }

    }
}