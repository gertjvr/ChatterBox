﻿using System;
using ChatterBox.ChatServer.Handlers.Users;
using ChatterBox.Core.Persistence;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.MessageContracts.Users.Commands;
using NSubstitute;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoNSubstitute;
using Shouldly;

namespace ChatterBox.ChatServer.Tests.Handlers.Users
{
    public class WhenChangeUserNameCommandHandler : AutoSpecFor<ChangeUserNameCommandHandler>
    {
        protected ChangeUserNameCommand Command;
        protected User User;
        
        protected IUnitOfWork UnitOfWork;
        protected IRepository<User> Repository;

        public WhenChangeUserNameCommandHandler()
            : base(new Fixture().Customize(new AutoNSubstituteCustomization()))
        {   
        }

        protected override ChangeUserNameCommandHandler Given()
        {
            User = Fixture.Freeze<User>();
            
            Repository = Fixture.Freeze<IRepository<User>>();
            Repository.GetById(Arg.Is(User.Id)).Returns(User);

            UnitOfWork = Fixture.Freeze<IUnitOfWork>();
            UnitOfWork.Repository<User>().Returns(Repository);

            return new ChangeUserNameCommandHandler(() => UnitOfWork);
        }

        protected override async void When()
        {
            Command = Fixture.Create<ChangeUserNameCommand>();

            await Subject.Handle(Command);
        }

        [Then]
        public void UserNameChangedCorrectly()
        {
            User.Name.ShouldBe(Command.NewUserName);
        }

        [Then]
        public void UnitOfWorkCompletedSuccesful()
        {
            UnitOfWork.Received(1).Complete();
        } 
    }
}