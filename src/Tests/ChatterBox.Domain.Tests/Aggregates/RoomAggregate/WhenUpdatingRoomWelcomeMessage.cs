using ChatterBox.Core.Tests.Specifications;
using ChatterBox.Domain.Aggregates.RoomAggregate;
using Ploeh.AutoFixture;
using Shouldly;

namespace ChatterBox.Domain.Tests.Aggregates.RoomAggregate
{
    public class WhenUpdatingRoomWelcomeMessage : AutoSpecificationFor<Room>
    {
        public string NewWelcomeMessage { get; private set; }

        protected override Room Given()
        {
            NewWelcomeMessage = Fixture.Create<string>();

            return Fixture.Create<Room>();
        }

        protected override void When()
        {
            Subject.UpdateWelcomeMessage(NewWelcomeMessage);
        }

        [Then]
        public void ShouldHaveUpdatedWelcomeMessage()
        {
            Subject.WelcomeMessage.ShouldBe(NewWelcomeMessage);
        }
    }
}