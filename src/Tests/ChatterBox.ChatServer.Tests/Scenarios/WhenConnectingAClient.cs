using System;
using System.Threading.Tasks;
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
    public class WhenConnectingAClient : AutoSpecificationForAsync<ConnectClientCommandHandler>
    {
        protected User CallingUser;

        protected ConnectClientCommand Command;

        protected override async Task<ConnectClientCommandHandler> Given()
        {
            CallingUser = Fixture.Create<User>();
            
            Command = new ConnectClientCommand(Fixture.Create<Guid>(), Fixture.Create<string>(), CallingUser.Id);

            var repository = Fixture.Freeze<IRepository<User>>();
            repository.GetById(Arg.Is(CallingUser.Id))
                .Returns(CallingUser);

            return Fixture.Create<ConnectClientCommandHandler>();
        }

        protected override async Task When()
        {
            await Subject.Handle(Command);
        }
    }
}