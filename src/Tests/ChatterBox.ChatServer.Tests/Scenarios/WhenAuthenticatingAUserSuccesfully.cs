using System;
using System.Threading.Tasks;
using ChatterBox.ChatServer.Handlers;
using ChatterBox.Core.Extensions;
using ChatterBox.Core.Persistence;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.Domain.Queries;
using ChatterBox.MessageContracts.Requests;
using NSubstitute;
using Ploeh.AutoFixture;
using Shouldly;
using SpecificationFor;
using SpecificationFor.AutoFixture;

namespace ChatterBox.ChatServer.Tests.Scenarios
{
    public class WhenAuthenticatingAUserWithUserNameAndPassword : AutoAsyncSpecFor<AuthenticateUserRequestHandler>
    {
        protected User User;

        protected IRepository<User> Repository;
        protected IUnitOfWork UnitOfWork;

        protected AuthenticateUserRequest Request;
        protected AuthenticateUserResponse Response;

        protected override async Task<AuthenticateUserRequestHandler> Given()
        {
            var salt = Fixture.Create<string>();
            var password = Fixture.Create<string>();
            var hashedPassword = password.ToSha256(salt);

            Request = Fixture.Build<AuthenticateUserRequest>()
                .With(p => p.Password, password)
                .Create();

            User = Fixture.Build<User>()
                .Do(u => u.ChangeSalt(salt))
                .Do(u => u.ChangePassword(hashedPassword))
                .Create();

            Repository = Fixture.Freeze<IRepository<User>>();
            Repository.Query(Arg.Any<GetUserIdByNameQuery>())
                .Returns(User.Id);

            Repository.GetById(Arg.Any<Guid>())
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

        [Then]
        public void ShouldReturnCorrectUserId()
        {
            Response.UserId.ShouldBe(User.Id);
        }

        [Then]
        public void ShouldReturnClientId()
        {
            Response.ClientId.ShouldNotBe(Guid.Empty);
        }
    }
}