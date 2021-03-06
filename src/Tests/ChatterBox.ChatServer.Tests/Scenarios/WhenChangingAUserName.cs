﻿using System.Threading.Tasks;
using ChatterBox.ChatServer.Handlers.Users;
using ChatterBox.Core.Infrastructure;
using ChatterBox.Core.Tests.Specifications;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.MessageContracts.Users.Commands;
using NSubstitute;
using Ploeh.AutoFixture;
using Shouldly;

namespace ChatterBox.ChatServer.Tests.Scenarios
{
    public class WhenChangingAUserName : AutoSpecificationForAsync<ChangeUserNameCommandHandler>
    {
        protected ChangeUserNameCommand Command;
        protected User User;

        protected IUnitOfWork UnitOfWork;
        protected IRepository<User> Repository;

        protected override async Task<ChangeUserNameCommandHandler> Given()
        {
            User = Fixture.Freeze<User>();

            var newUserName = Fixture.Create<string>();

            Command = new ChangeUserNameCommand(User.Id, newUserName, User.Id);

            Repository = Fixture.Freeze<IRepository<User>>();
            
            Repository.GetById(Arg.Is(User.Id))
                .Returns(User);

            UnitOfWork = Fixture.Freeze<IUnitOfWork>();
            
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