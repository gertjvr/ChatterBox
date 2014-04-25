using ChatterBox.Core.Tests.Specifications;
using ChatterBox.Domain.Aggregates.UserAggregate;
using Ploeh.AutoFixture;
using Shouldly;

namespace ChatterBox.Domain.Tests.Aggregates.UserAggregate
{
    public class WhenUpdatingUserEmailAddress : AutoSpecificationFor<User>
    {
        public string NewEmailAddress { get; private set; }

        protected override User Given()
        {
            NewEmailAddress = Fixture.Create<string>();

            return Fixture.Create<User>();
        }

        protected override void When()
        {
            Subject.UpdateEmailAddress(NewEmailAddress);
        }

        [Then]
        public void ContainsCorrectPendingFact()
        {
            Subject.EmailAddress.ShouldBe(NewEmailAddress);
        }
    }
}