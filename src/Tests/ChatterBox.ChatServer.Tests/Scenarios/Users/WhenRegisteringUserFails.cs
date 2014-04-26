using System;
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

namespace ChatterBox.ChatServer.Tests.Scenarios.Users
{
    public class WhenRegisteringUserFails : AutoSpecificationForAsync<RegisterUserRequestHandler>
    {
        public RegisterUserRequest Request { get; private set; }
        public RegisterUserResponse Response { get; private set; }

        public IUnitOfWork UnitOfWork { get; private set; }
        public IRepository<User> Repository { get; private set; }

        protected override async Task<RegisterUserRequestHandler> Given()
        {
            Request = Fixture.Create<RegisterUserRequest>();
            Request.SetPropertyValue(p => p.Password, null);

            Repository = Fixture.Freeze<IRepository<User>>();

            UnitOfWork = Fixture.Freeze<IUnitOfWork>();
            
            return Fixture.Create<RegisterUserRequestHandler>();
        }

        protected override async Task When()
        {
            Should.Throw<Exception>(async () => Response = await Subject.Handle(Request));
        }

        [Then]
        public void ShouldHaveAbandonUnitOfWork()
        {
            UnitOfWork.Received(1).Abandon();
        }
    }
}