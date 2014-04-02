using System;
using ChatterBox.ChatServer.Handlers;
using ChatterBox.Core.Persistence;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.MessageContracts.Requests;
using NSubstitute;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoNSubstitute;
using Shouldly;

namespace ChatterBox.ChatServer.Tests.Scenarios
{
    public class WhenCreatingAUser : AutoSpecFor<CreateUserRequestHandler>
    {
        protected CreateUserRequest Request;
        protected CreateUserResponse Response;

        protected IUnitOfWork UnitOfWork;
        protected IRepository<User> Repository;
        
        public WhenCreatingAUser()
            : base(() => new Fixture().Customize(new AutoNSubstituteCustomization()))
        {   
        }

        protected override CreateUserRequestHandler Given()
        {
            Repository = Fixture.Freeze<IRepository<User>>();

            UnitOfWork = Fixture.Freeze<IUnitOfWork>();
            UnitOfWork.Repository<User>().Returns(Repository);

            return Fixture.Create<CreateUserRequestHandler>();
        }

        protected override async void When()
        {
            Request = Fixture.Create<CreateUserRequest>();

            Response = await Subject.Handle(Request);
        }

        [Then]
        public void ReturnsCorrectUserId()
        {
            Response.UserId.ShouldNotBe(Guid.Empty);
        }

        [Then]
        public void UserWasPersisted()
        {
            Repository.Received(1).Add(Arg.Any<User>());
        }

        [Then]
        public void UnitOfWorkCompletedSuccesful()
        {
            UnitOfWork.Received(1).Complete();
        }
    }
}