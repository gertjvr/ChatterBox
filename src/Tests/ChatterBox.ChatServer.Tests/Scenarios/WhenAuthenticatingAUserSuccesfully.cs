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
using ChatterBox.Domain.Extensions;
using ChatterBox.MessageContracts.Authentication.Request;
using ChatterBox.MessageContracts.Dtos;
using NSubstitute;
using Ploeh.AutoFixture;
using Shouldly;

namespace ChatterBox.ChatServer.Tests.Scenarios
{
    public class WhenAuthenticatingAUserWithUserNameAndPassword : AutoSpecificationForAsync<AuthenticateUserRequestHandler>
    {
        protected User User;

        protected AuthenticateUserRequest Request;
        protected AuthenticateUserResponse Response;

        protected override async Task<AuthenticateUserRequestHandler> Given()
        {
            var userName = Fixture.Create<string>();
            var salt = Fixture.Create<string>();
            var password = Fixture.Create<string>();
            var hashedPassword = password.ToSha256(salt);

            Request = new AuthenticateUserRequest(userName, password);

            User = Fixture.Build<User>()
                .Do(u => u.UpdateSalt(salt))
                .Do(u => u.UpdatePassword(hashedPassword))
                .Create();

            var userDto = new UserDto(
                User.Name, 
                User.Hash, 
                User.LastActivity, 
                (int)User.Status, 
                (int)User.UserRole);

            var repository = Fixture.Freeze<IRepository<User>>();
            repository.Query(Arg.Any<Func<IQueryable<User>, User>>())
                .Returns(User);
            
            var userMapper = Fixture.Freeze<IMapToNew<User, UserDto>>();
            userMapper.Map(Arg.Is(User)).Returns(userDto);

            var cryptoService = Fixture.Freeze<ICryptoService>();
            cryptoService.CreateSalt().Returns(Fixture.Create<string>());

            return Fixture.Create<AuthenticateUserRequestHandler>();
        }

        protected override async Task When()
        {
            Response = await Subject.Handle(Request);
        }

        public void ShouldReturnCorrectUserId()
        {
            Response.UserId.ShouldBe(User.Id);
        }

        public void ShouldReturnClientId()
        {
            Response.ClientId.ShouldNotBe(Guid.Empty);
        }
    }
}