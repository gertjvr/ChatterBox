using ChatterBox.Core.Tests.Specifications;
using ChatterBox.Domain.Aggregates.UserAggregate;
using Ploeh.AutoFixture;
using Shouldly;

namespace ChatterBox.Domain.Tests.Aggregates.UserAggregate
{
    public class WhenUpdatingUserSalt : AutoSpecificationFor<User>
    {
        public string NewSalt { get; private set; }

        protected override User Given()
        {
            NewSalt = Fixture.Create<string>();

            return Fixture.Create<User>();
        }

        protected override void When()
        {
            Subject.UpdateSalt(NewSalt);
        }

        [Then]
        public void ContainsCorrectPendingFact()
        {
            Subject.Salt.ShouldBe(NewSalt);
        }
    }
}