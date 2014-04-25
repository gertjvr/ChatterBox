using System;
using ChatterBox.Core.Tests.Specifications;
using ChatterBox.Domain.Aggregates.ClientAggregate;
using Ploeh.AutoFixture;
using Shouldly;

namespace ChatterBox.Domain.Tests.Aggregates.ClientAggregate
{
    public class WhenUpdatingClientLastActivity : AutoSpecificationFor<Client>
    {
        public DateTimeOffset NewActivity { get; private set; }

        protected override Client Given()
        {
            NewActivity = Fixture.Create<DateTimeOffset>();

            return Fixture.Create<Client>();
        }

        protected override void When()
        {
            Subject.UpdateLastActivity(NewActivity);
        }

        [Then]
        public void ContainsCorrectPendingFact()
        {
            Subject.LastActivity.ShouldBe(NewActivity);
        }
    }
}