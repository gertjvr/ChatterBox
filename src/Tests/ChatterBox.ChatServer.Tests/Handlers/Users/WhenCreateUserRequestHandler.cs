using System;
using ChatterBox.ChatServer.Handlers.Users;
using ChatterBox.MessageContracts.Users.Requests;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoNSubstitute;
using Shouldly;

namespace ChatterBox.ChatServer.Tests.Handlers.Users
{
    public class WhenCreateUserRequestHandler : AutoSpecFor<CreateUserRequestHandler>
    {
        protected CreateUserRequest CreateUserRequest;
        protected CreateUserResponse CreateUserResponse;

        public WhenCreateUserRequestHandler()
            : base(new Fixture().Customize(new AutoNSubstituteCustomization()))
        {
            
        }


        protected override CreateUserRequestHandler Given()
        {
            return Fixture.Create<CreateUserRequestHandler>();
        }

        protected override async void When()
        {
            CreateUserRequest = Fixture.Create<CreateUserRequest>();

            CreateUserResponse = await Subject.Handle(CreateUserRequest);
        }

        [Then]
        public void Should()
        {
            CreateUserResponse.UserId.ShouldNotBe(Guid.Empty);
        }
    }
}
