using ChatterBox.Core.Tests.Specifications;
using ChatterBox.Domain.Aggregates.UserAggregate;
using Ploeh.AutoFixture;
using Shouldly;

namespace ChatterBox.Domain.Tests.Aggregates.UserAggregate
{
    public class WhenUpdatingUserRole : AutoSpecificationFor<User>
    {
        public UserRole NewRole { get; private set; }

        protected override User Given()
        {
            return Fixture.Create<User>();
        }

        protected override void When()
        {
            Subject.UpdateRole(NewRole);
        }

        [Then]
        public void ShouldHaveUpdatedUserRole()
        {
            Subject.Role.ShouldBe(NewRole);
        }
    }
}