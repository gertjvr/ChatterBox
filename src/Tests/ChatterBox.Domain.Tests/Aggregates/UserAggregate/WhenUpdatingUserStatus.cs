using ChatterBox.Core.Tests.Specifications;
using ChatterBox.Domain.Aggregates.UserAggregate;
using Ploeh.AutoFixture;
using Shouldly;

namespace ChatterBox.Domain.Tests.Aggregates.UserAggregate
{
    public class WhenUpdatingUserStatus : AutoSpecificationFor<User>
    {
        public UserStatus NewStatus { get; private set; }

        protected override User Given()
        {
            NewStatus = Fixture.Create<UserStatus>();

            return Fixture.Create<User>();
        }

        protected override void When()
        {
            Subject.UpdateStatus(NewStatus);
        }

        [Then]
        public void ShouldHaveUpdatedUserStatus()
        {
            Subject.Status.ShouldBe(NewStatus);
        }
    }
}