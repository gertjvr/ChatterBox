using ChatterBox.Core.Tests.Specifications;
using ChatterBox.Domain.Aggregates.RoomAggregate;
using ChatterBox.Domain.Aggregates.UserAggregate;
using Ploeh.AutoFixture;
using Shouldly;

namespace ChatterBox.Domain.Tests.Aggregates.RoomAggregate
{
    public class WhenUnallowingUserToRoom : AutoSpecificationFor<Room>
    {
        public User User { get; private set; }

        protected override Room Given()
        {
            User = Fixture.Create<User>();

            return Fixture.Create<Room>();
        }

        protected override void When()
        {
            Subject.UnallowUser(User);
        }

        [Then]
        public void ShouldNotContainerAllowedUser()
        {
            Subject.AllowedUsers.ShouldNotContain(User.Id);
        }
    }
}