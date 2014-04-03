﻿using System;
using ChatterBox.ChatServer.Handlers;
using ChatterBox.Core.Persistence;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.MessageContracts.Requests;
using NSubstitute;
using Ploeh.AutoFixture;
using Shouldly;

namespace ChatterBox.ChatServer.Tests.Scenarios
{
    public class WhenCreatingAUser : AutoSpecFor<CreateUserRequestHandler>
    {
        protected CreateUserRequest Request;
        protected CreateUserResponse Response;

        protected IUnitOfWork UnitOfWork;
        protected IRepository<User> Repository;

        protected override CreateUserRequestHandler Given()
        {
            Request = Fixture.Create<CreateUserRequest>();

            Repository = Fixture.Freeze<IRepository<User>>();

            UnitOfWork = Fixture.Freeze<IUnitOfWork>();
            UnitOfWork.Repository<User>()
                .Returns(Repository);

            return Fixture.Create<CreateUserRequestHandler>();
        }

        protected override async void When()
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