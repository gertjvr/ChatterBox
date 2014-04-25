using ChatterBox.Core.Tests.Specifications;
using ChatterBox.Domain.Aggregates.RoomAggregate;
using ChatterBox.Domain.Aggregates.UserAggregate;
using Ploeh.AutoFixture;
using Shouldly;

namespace ChatterBox.Domain.Tests.Aggregates.RoomAggregate
{
    public class WhenDeregisteringOwnerForRoom : AutoSpecificationFor<Room>
    {
        public User User { get; private set; }

        protected override Room Given()
        {
            User = Fixture.Create<User>();

            return Fixture.Create<Room>();
        }

        protected override void When()
        {
            Subject.DeregisterOwner(User);
        }

        [Then]
        public void ShouldHaveDeregisteredOwner()
        {
            Subject.Owners.ShouldNotContain(User.Id);
        }
    }
}