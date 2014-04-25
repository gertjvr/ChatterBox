using System;
using ChatterBox.Core.Tests.Specifications;
using ChatterBox.Domain.Aggregates.UserAggregate;
using Ploeh.AutoFixture;
using Shouldly;

namespace ChatterBox.Domain.Tests.Aggregates.UserAggregate
{
    public class WhenReceivingPrivateMessage : AutoSpecificationFor<User>
    {
        public string Content { get; private set; }

        public User User { get; private set; }

        public DateTimeOffset ReceivedAt { get; private set; }

        protected override User Given()
        {
            Content = Fixture.Create<string>();
            User = Fixture.Create<User>();
            ReceivedAt = Fixture.Create<DateTimeOffset>();

            return Fixture.Create<User>();
        }

        protected override void When()
        {
            Subject.ReceivePrivateMessage(Content, User, ReceivedAt);
        }

        [Then]
        public void ShouldContainPrivateMessage()
        {
            Subject.PrivateMessages().ShouldContain(message => message.Content == Content && message.UserId == User.Id && message.ReceivedAt == ReceivedAt);
        }
    }
}