using ChatterBox.Core.Tests.Specifications;
using ChatterBox.Domain.Aggregates.RoomAggregate;
using Ploeh.AutoFixture;
using Shouldly;

namespace ChatterBox.Domain.Tests.Aggregates.RoomAggregate
{
    public class WhenOpeningRoom : AutoSpecificationFor<Room>
    {
        protected override Room Given()
        {
            return Fixture.Create<Room>();
        }

        protected override void When()
        {
            Subject.Open();
        }

        [Then]
        public void ShouldHaveOpenedRoom()
        {
            Subject.Closed.ShouldBe(false);
        }
    }
}