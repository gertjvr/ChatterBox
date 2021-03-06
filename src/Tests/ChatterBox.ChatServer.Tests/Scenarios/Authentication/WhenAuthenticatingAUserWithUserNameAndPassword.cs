using System;
using System.Linq;
using System.Threading.Tasks;
using ChatterBox.ChatServer.Handlers.Authentication;
using ChatterBox.ChatServer.Infrastructure.Mappers;
using ChatterBox.Core.Extensions;
using ChatterBox.Core.Infrastructure;
using ChatterBox.Core.Mapping;
using ChatterBox.Core.Services;
using ChatterBox.Core.Tests;
using ChatterBox.Core.Tests.Specifications;
using ChatterBox.Domain.Aggregates.RoomAggregate;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.MessageContracts.Authentication.Request;
using ChatterBox.MessageContracts.Dtos;
using NSubstitute;
using Ploeh.AutoFixture;
using Shouldly;
using ThirdDrawer.Extensions.StringExtensionMethods;

namespace ChatterBox.ChatServer.Tests.Scenarios.Authentication
{
    public class WhenAuthenticatingAUserWithUserNameAndPassword : AutoSpecificationForAsync<AuthenticateUserRequestHandler>
    {
        protected User User;
        protected IClock Clock;

        protected IRepository<User> UserRepository;
        protected IRepository<Room> RoomRepository;
        protected IUnitOfWork UnitOfWork;

        protected AuthenticateUserRequest Request;
        protected AuthenticateUserResponse Response;

        protected string UserName;
        protected string EmailAddress;
        protected string Salt;
        protected string Password;

        protected override async Task<AuthenticateUserRequestHandler> Given()
        {
            Clock = Fixture.Freeze<IClock>();
            Clock.UtcNow.Returns(DateTimeOffset.UtcNow);
            
            UserName = "userName{0}".FormatWith(Fixture.Create<string>());
            EmailAddress = "emailAddress{0}".FormatWith(Fixture.Create<string>());
            Salt = "salt{0}".FormatWith(Fixture.Create<string>());
            Password = "password{0}".FormatWith(Fixture.Create<string>());

            User = ObjectMother.CreateUser(UserName, EmailAddress, Salt, Password, Clock.UtcNow);

            Request = new AuthenticateUserRequest(UserName, Password);

            UnitOfWork = Fixture.Freeze<IUnitOfWork>();

            UserRepository = Fixture.Freeze<IRepository<User>>();
            UserRepository.Query(Arg.Any<Func<IQueryable<User>, User>>())
                .Returns(User);

            RoomRepository = Fixture.Freeze<IRepository<Room>>();
            RoomRepository.Query(Arg.Any<Func<IQueryable<Room>, Room[]>>())
                .Returns(new [] { Fixture.Create<Room>() });

            Fixture.Inject<IMapToNew<User, UserDto>>(Fixture.Create<UserToUserDtoMapper>());

            var cryptoService = Fixture.Freeze<ICryptoService>();
            cryptoService.CreateSalt().Returns(Fixture.Create<string>());

            return Fixture.Create<AuthenticateUserRequestHandler>();
        }

        protected override async Task When()
        {
            Response = await Subject.Handle(Request);
        }

        [Then]
        public void ShouldNotCreatedAdminUser()
        {
            UserRepository.DidNotReceive().Add(Arg.Any<User>());
            UnitOfWork.Received(1).Complete();
        }

        [Then]
        public void ShouldHaveUserName()
        {
            Response.User.Name.ShouldBe(UserName);
        }

        [Then]
        public void ShouldHaveHash()
        {
            Response.User.Hash.ShouldBe(EmailAddress.ToMD5());
        }

        [Then]
        public void ShouldHaveAdminUserRole()
        {
            Response.User.Role.ShouldBe((int)User.Role);
        }

        [Then]
        public void ShouldHaveUserId()
        {
            Response.UserId.ShouldNotBe(Guid.Empty);
        }

    }
}