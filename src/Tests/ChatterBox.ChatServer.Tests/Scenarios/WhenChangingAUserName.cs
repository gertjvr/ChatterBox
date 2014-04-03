using System;
using ChatterBox.ChatServer.Handlers;
using ChatterBox.Core.Persistence;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.MessageContracts.Commands;
using NSubstitute;
using Ploeh.AutoFixture;
using Shouldly;

namespace ChatterBox.ChatServer.Tests.Scenarios
{
    public class WhenChangingAUserName : AutoSpecFor<ChangeUserNameCommandHandler>
    {
        protected ChangeUserNameCommand Command;
        protected User User;

        protected IUnitOfWork UnitOfWork;
        protected IRepository<User> Repository;

        protected override ChangeUserNameCommandHandler Given()
        {
            Command = Fixture.Create<ChangeUserNameCommand>();

            User = Fixture.Freeze<User>();

            Repository = Fixture.Freeze<IRepository<User>>();
            Repository.GetById(Arg.Any<Guid>())
                .Returns(User);

            UnitOfWork = Fixture.Freeze<IUnitOfWork>();
            UnitOfWork.Repository<User>()
                .Returns(Repository);

            return Fixture.Create<ChangeUserNameCommandHandler>();
        }

        protected override async void When()
        {
            await Subject.Handle(Command);
        }

        [Then]
        public void ShouldHaveChangedUserName()
        {
            User.Name.ShouldBe(Command.NewUserName);
        }

        [Then]
        public void ShouldHaveCompletedUnitOfWork()
        {
            UnitOfWork.Received(1).Complete();
        } 
    }
}