using System;
using System.Threading.Tasks;
using ChatterBox.ChatServer.Handlers;
using ChatterBox.Core.Persistence;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.MessageContracts.Commands;
using NSubstitute;
using Ploeh.AutoFixture;
using Shouldly;
using SpecificationFor;
using SpecificationFor.AutoFixture;

namespace ChatterBox.ChatServer.Tests.Scenarios
{
    public class WhenChangingAUserName : AutoAsyncSpecFor<ChangeUserNameCommandHandler>
    {
        protected ChangeUserNameCommand Command;
        protected User User;

        protected IUnitOfWork UnitOfWork;
        protected IRepository<User> Repository;

        protected override async Task<ChangeUserNameCommandHandler> Given()
        {
            User = Fixture.Freeze<User>();
            
            Command = Fixture.Build<ChangeUserNameCommand>()
                .With(p => p.UserId, User.Id)
                .Create();

            Repository = Fixture.Freeze<IRepository<User>>();
            
            Repository.GetById(Arg.Is(User.Id))
                .Returns(User);

            UnitOfWork = Fixture.Freeze<IUnitOfWork>();
            UnitOfWork.Repository<User>()
                .Returns(Repository);

            return Fixture.Create<ChangeUserNameCommandHandler>();
        }

        protected override async Task When()
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