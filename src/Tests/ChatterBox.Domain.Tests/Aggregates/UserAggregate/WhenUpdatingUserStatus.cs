using ChatterBox.Core.Tests.Specifications;
using ChatterBox.Domain.Aggregates.UserAggregate;
using Ploeh.AutoFixture;
using Shouldly;

namespace ChatterBox.Domain.Tests.Aggregates.UserAggregate
{
    public class WhenUpdatingUserStatus : AutoSpecificationFor<User>
    {
        public UserStatus NewUserStatus { get; private set; }

        protected override User Given()
        {
            NewUserStatus = Fixture.Create<UserStatus>();

            return Fixture.Create<User>();
        }

        protected override void When()
        {
            Subject.UpdateStatus(NewUserStatus);
        }

        [Then]
        public void ContainsCorrectPendingFact()
        {
            Subject.Status.ShouldBe(NewUserStatus);
        }
    }
}