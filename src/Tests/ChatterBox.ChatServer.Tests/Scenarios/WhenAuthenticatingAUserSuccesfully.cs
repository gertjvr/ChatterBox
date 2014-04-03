using System;
using System.Linq;
using ChatterBox.ChatServer.Handlers;
using ChatterBox.Core.Extensions;
using ChatterBox.Core.Persistence;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.Domain.Queries;
using ChatterBox.MessageContracts.Requests;
using NSubstitute;
using Ploeh.AutoFixture;
using Shouldly;

namespace ChatterBox.ChatServer.Tests.Scenarios
{
    public class WhenAuthenticatingAUserWithUserNameAndPassword : AutoSpecFor<AuthenticateUserRequestHandler>
    {
        protected User[] Users;

        protected IRepository<User> Repository;
        protected IUnitOfWork UnitOfWork;

        protected AuthenticateUserRequest Request;
        protected AuthenticateUserResponse Response;

        protected override AuthenticateUserRequestHandler Given()
        {
            var salt = Fixture.Create<string>();
            var password = Fixture.Create<string>();
            var hashedPassword = password.ToSha256(salt);

            Request = Fixture.Build<AuthenticateUserRequest>()
                .With(p => p.Password, password)
                .Create();

            Users = Fixture.Build<User>()
                .Do(u => u.ChangeSalt(salt))
                .Do(u => u.ChangePassword(hashedPassword))
                .CreateMany(1)
                .ToArray();

            Repository = Fixture.Freeze<IRepository<User>>();
            Repository.Query(Arg.Any<GetUserByNameQuery>())
                .Returns(Users);

            UnitOfWork = Fixture.Freeze<IUnitOfWork>();
            UnitOfWork.Repository<User>()
                .Returns(Repository);

            return Fixture.Create<AuthenticateUserRequestHandler>();
        }

        protected override async void When()
        {
            Response = await Subject.Handle(Request);
        }

        [Then]
        public void ShouldHaveAuthenticatedTheUser()
        {
            Response.IsAutenticated.ShouldBe(true);
        }

        [Then]
        public void ShouldReturnCorrectUserId()
        {
            Response.UserId.ShouldNotBe(Guid.Empty);
        }
    }
}