using System;
using System.Threading.Tasks;
using ChatterBox.ChatServer.Handlers;
using ChatterBox.ChatServer.Handlers.Authentication;
using ChatterBox.Core.Extensions;
using ChatterBox.Core.Tests;
using ChatterBox.Core.Persistence;
using ChatterBox.Core.Tests.Specifications;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.Domain.Queries;
using ChatterBox.MessageContracts;
using ChatterBox.MessageContracts.Authentication.Request;
using NSubstitute;
using Ploeh.AutoFixture;
using Shouldly;

namespace ChatterBox.ChatServer.Tests.Scenarios
{
    public class WhenAuthenticatingAUserWithUserNameAndPassword : AutoSpecificationForAsync<AuthenticateUserRequestHandler>
    {
        protected User User;

        protected IRepository<User> Repository;
        protected IUnitOfWork UnitOfWork;

        protected AuthenticateUserRequest Request;
        protected AuthenticateUserResponse Response;

        protected override async Task<AuthenticateUserRequestHandler> Given()
        {
            var userName = Fixture.Create<string>();
            var salt = Fixture.Create<string>();
            var password = Fixture.Create<string>();
            var hashedPassword = password.ToSha256(salt);

            Request = new AuthenticateUserRequest(userName, password);

            User = Fixture.Build<User>()
                .Do(u => u.UpdateSalt(salt))
                .Do(u => u.UpdatePassword(hashedPassword))
                .Create();

            Repository = Fixture.Freeze<IRepository<User>>();
            Repository.Query(Arg.Any<UserIdByNameQuery>())
                .Returns(User.Id);

            Repository.GetById(Arg.Is(User.Id))
                .Returns(User);

            UnitOfWork = Fixture.Freeze<IUnitOfWork>();
            UnitOfWork.Repository<User>()
                .Returns(Repository);

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

        public void ShouldReturnClientId()
        {
            Response.ClientId.ShouldNotBe(Guid.Empty);
        }
    }
}