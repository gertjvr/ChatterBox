using System;
using System.Threading.Tasks;
using ChatterBox.ChatServer.Handlers.Users;
using ChatterBox.ChatServer.Infrastructure.Mappers;
using ChatterBox.Core.Infrastructure;
using ChatterBox.Core.Mapping;
using ChatterBox.Core.Tests.Specifications;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.MessageContracts.Dtos;
using ChatterBox.MessageContracts.Users.Requests;
using NSubstitute;
using Ploeh.AutoFixture;
using Shouldly;

namespace ChatterBox.ChatServer.Tests.Scenarios.Users
{
    public class WhenRegisteringUser : AutoSpecificationForAsync<RegisterUserRequestHandler>
    {
        protected RegisterUserRequest Request;
        protected RegisterUserResponse Response;

        protected IUnitOfWork UnitOfWork;
        protected IRepository<User> Repository;

        protected override async Task<RegisterUserRequestHandler> Given()
        {
            Request = Fixture.Create<RegisterUserRequest>();
            Repository = Fixture.Freeze<IRepository<User>>();
            UnitOfWork = Fixture.Freeze<IUnitOfWork>();

            Fixture.Inject<IMapToNew<User, UserDto>>(Fixture.Create<UserToUserDtoMapper>());
            
            return Fixture.Create<RegisterUserRequestHandler>();
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