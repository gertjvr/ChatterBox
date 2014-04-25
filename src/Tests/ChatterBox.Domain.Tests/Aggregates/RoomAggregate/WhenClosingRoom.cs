using ChatterBox.Core.Tests.Specifications;
using ChatterBox.Domain.Aggregates.RoomAggregate;
using Ploeh.AutoFixture;
using Shouldly;

namespace ChatterBox.Domain.Tests.Aggregates.RoomAggregate
{
    public class WhenClosingRoom : AutoSpecificationFor<Room>
    {
        protected override Room Given()
        {
            return Fixture.Create<Room>();
        }

        protected override void When()
        {
            Subject.Close();
        }

        [Then]
        public void ShouldHaveClosedRoom()
        {
            Subject.Closed.ShouldBe(true);
        }
    }
}