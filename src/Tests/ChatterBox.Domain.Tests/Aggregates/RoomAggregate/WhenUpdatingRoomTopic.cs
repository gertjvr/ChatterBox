using ChatterBox.Core.Tests.Specifications;
using ChatterBox.Domain.Aggregates.RoomAggregate;
using Ploeh.AutoFixture;
using Shouldly;

namespace ChatterBox.Domain.Tests.Aggregates.RoomAggregate
{
    public class WhenUpdatingRoomTopic : AutoSpecificationFor<Room>
    {
        public string NewTopic { get; private set; }

        protected override Room Given()
        {
            NewTopic = Fixture.Create<string>();

            return Fixture.Create<Room>();
        }

        protected override void When()
        {
            Subject.UpdateTopic(NewTopic);
        }

        [Then]
        public void ShouldHaveUpdatedRoomTopic()
        {
            Subject.Topic.ShouldBe(NewTopic);
        }
    }
}