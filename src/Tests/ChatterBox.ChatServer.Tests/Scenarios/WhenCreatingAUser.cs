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
    public class WhenCreatingAUser : AutoSpecificationForAsync<CreateUserRequestHandler>
    {
        protected CreateUserRequest Request;
        protected CreateUserResponse Response;

        protected IUnitOfWork UnitOfWork;
        protected IRepository<User> Repository;

        protected override async Task<CreateUserRequestHandler> Given()
        {
            Request = Fixture.Create<CreateUserRequest>();
            Repository = Fixture.Freeze<IRepository<User>>();
            UnitOfWork = Fixture.Freeze<IUnitOfWork>();
            
            return Fixture.Create<CreateUserRequestHandler>();
        }

        protected override async Task When()
        {
            Response = await Subject.Handle(Request);
        }

        [Then]
        public void ShouldReturnsCorrectUserId()
        {
            Response.UserId.ShouldNotBe(Guid.Empty);
        }

        [Then]
        public void ShouldHaveUserWasPersisted()
        {
            Repository.Received(1).Add(Arg.Any<User>());
        }

        [Then]
        public void ShouldHaveCompletedUnitOfWork()
        {
            UnitOfWork.Received(1).Complete();
        }
    }
}