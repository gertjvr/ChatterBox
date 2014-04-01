﻿using System;
using ChatterBox.ChatServer.Handlers.Users;
using ChatterBox.Core.Persistence;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.MessageContracts.Users.Requests;
using NSubstitute;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoNSubstitute;
using Shouldly;

namespace ChatterBox.ChatServer.Tests.Handlers.Users
{
    public class WhenCreateUserRequestHandler : AutoSpecFor<CreateUserRequestHandler>
    {
        protected CreateUserRequest Request;
        protected CreateUserResponse Response;

        protected User User;
        protected IUnitOfWork UnitOfWork;
        protected IRepository<User> Repository; 

        public WhenCreateUserRequestHandler()
            : base(new Fixture().Customize(new AutoNSubstituteCustomization()))
        {   
        }

        protected override CreateUserRequestHandler Given()
        {
            User = Fixture.Freeze<User>();

            Repository = Fixture.Freeze<IRepository<User>>();
            Repository.GetById(Arg.Any<Guid>()).Returns(Fixture.Create<User>());

            UnitOfWork = Fixture.Freeze<IUnitOfWork>();

            return new CreateUserRequestHandler(() => UnitOfWork);
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
        public void UnitOfWorkCompletedSuccesful()
        {
            UnitOfWork.Received(1).Complete();
        }
    }
}