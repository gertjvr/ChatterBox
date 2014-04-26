using System;
using System.Linq;
using System.Threading.Tasks;
using ChatterBox.ChatServer.Handlers.Users;
using ChatterBox.Core.Infrastructure;
using ChatterBox.Core.Tests.Extensions;
using ChatterBox.Core.Tests.Specifications;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.MessageContracts.Users.Requests;
using NSubstitute;
using Ploeh.AutoFixture;
using Shouldly;
using ThirdDrawer.Extensions.StringExtensionMethods;

namespace ChatterBox.ChatServer.Tests.Scenarios.Users
{
    public class WhenRegisteringUserFails : AutoSpecificationForAsync<RegisterUserRequestHandler>
    {
        public User User { get; private set; }
        public RegisterUserRequest Request { get; private set; }
        public RegisterUserResponse Response { get; private set; }

        public IUnitOfWork UnitOfWork { get; private set; }
        public IRepository<User> Repository { get; private set; }

        protected override async Task<RegisterUserRequestHandler> Given()
        {
            User = Fixture.Create<User>();

            Request = new RegisterUserRequest(User.Name, User.EmailAddress, Fixture.Create<string>());

            Repository = Fixture.Freeze<IRepository<User>>();
            Repository.Query(Arg.Any<Func<IQueryable<User>, User>>())
                .Returns(User);

            UnitOfWork = Fixture.Freeze<IUnitOfWork>();
            
            return Fixture.Create<RegisterUserRequestHandler>();
        }

        protected override async Task When()
        {
            Exception = Should.Throw<InvalidOperationException>(async () => Response = await Subject.Handle(Request));
        }

        public InvalidOperationException Exception { get; set; }

        [Then]
        public void ShouldHaveThrowInvalidOperationException()
        {
            Exception.Message.ShouldBe("UserName[{0}] has been already registered.".FormatWith(User.Name));   
        }

        [Then]
        public void ShouldHaveAbandonUnitOfWork()
        {
            UnitOfWork.Received(1).Abandon();
        }
    }
}