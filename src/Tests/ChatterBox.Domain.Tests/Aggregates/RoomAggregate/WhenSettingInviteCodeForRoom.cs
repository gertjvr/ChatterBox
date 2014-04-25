using ChatterBox.Core.Tests.Specifications;
using ChatterBox.Domain.Aggregates.RoomAggregate;
using Ploeh.AutoFixture;
using Shouldly;

namespace ChatterBox.Domain.Tests.Aggregates.RoomAggregate
{
    public class WhenSettingInviteCodeForRoom : AutoSpecificationFor<Room>
    {
        public string InviteCode { get; private set; }

        protected override Room Given()
        {
            InviteCode = Fixture.Create<string>();

            return Fixture.Create<Room>();
        }

        protected override void When()
        {
            Subject.SetInviteCode(InviteCode);
        }

        [Then]
        public void ShouldHaveSetInvideCode()
        {
            Subject.InviteCode.ShouldBe(InviteCode);
        }
    }
}