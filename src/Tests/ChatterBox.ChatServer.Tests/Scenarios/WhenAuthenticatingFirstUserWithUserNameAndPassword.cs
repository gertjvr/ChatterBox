using System;
using System.Linq;
using System.Threading.Tasks;
using ChatterBox.ChatServer.Handlers.Authentication;
using ChatterBox.Core.Extensions;
using ChatterBox.Core.Infrastructure;
using ChatterBox.Core.Mapping;
using ChatterBox.Core.Services;
using ChatterBox.Core.Tests.Specifications;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.MessageContracts.Authentication.Request;
using ChatterBox.MessageContracts.Dtos;
using NSubstitute;
using Ploeh.AutoFixture;
using Shouldly;
using ThirdDrawer.Extensions.StringExtensionMethods;

namespace ChatterBox.ChatServer.Tests.Scenarios
{
    public class WhenAuthenticatingFirstUserWithUserNameAndPassword :
        AutoSpecificationForAsync<AuthenticateUserRequestHandler>
    {
        protected IRepository<User> Repository;

        protected AuthenticateUserRequest Request;
        protected AuthenticateUserResponse Response;
        protected IUnitOfWork UnitOfWork;

        protected string EmailAddress;
        protected string Password;

        protected override async Task<AuthenticateUserRequestHandler> Given()
        {
            EmailAddress = "emailAddress{0}".FormatWith(Fixture.Create<string>());
            Password = "password{0}".FormatWith(Fixture.Create<string>());

            Request = new AuthenticateUserRequest(EmailAddress, Password);

            Repository = Fixture.Freeze<IRepository<User>>();

            Repository.Query(Arg.Any<Func<IQueryable<User>, bool>>())
                .Returns(true);

            var userMapper = Fixture.Freeze<IMapToNew<User, UserDto>>();
            userMapper.Map(Arg.Any<User>())
                .Returns(info =>
                {
                    var user = info.Arg<User>();
                    return new UserDto(
                        user.Name,
                        user.Hash,
                        user.LastActivity,
                        (int) user.Status,
                        (int) user.Role);
                });

            UnitOfWork = Fixture.Freeze<IUnitOfWork>();

            var cryptoService = Fixture.Freeze<ICryptoService>();
            cryptoService.CreateSalt()
                .Returns(Fixture.Create<string>());

            return Fixture.Create<AuthenticateUserRequestHandler>();
        }

        protected override async Task When()
        {
            Response = await Subject.Handle(Request);
        }

        [Then]
        public void ShouldHaveCreatedAdminUser()
        {
            Repository.Received(1).Add(Arg.Any<User>());
            UnitOfWork.Received(1).Complete();
        }

        [Then]
        public void ShouldHaveUserName()
        {
            Response.User.Name.ShouldBe("Admin");
        }

        [Then]
        public void ShouldHaveHash()
        {
            Response.User.Hash.ShouldBe(EmailAddress.ToMD5());
        }

        [Then]
        public void ShouldHaveAdminUserRole()
        {
            Response.User.UserRole.ShouldBe((int)UserRole.Admin);
        }

        [Then]
        public void ShouldHaveUserId()
        {
            Response.UserId.ShouldNotBe(Guid.Empty);
        }
    }
}