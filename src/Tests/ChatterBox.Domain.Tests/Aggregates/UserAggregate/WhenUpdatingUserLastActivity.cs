using System;
using ChatterBox.Core.Tests.Specifications;
using ChatterBox.Domain.Aggregates.UserAggregate;
using Ploeh.AutoFixture;
using Shouldly;

namespace ChatterBox.Domain.Tests.Aggregates.UserAggregate
{
    public class WhenUpdatingUserLastActivity : AutoSpecificationFor<User>
    {
        public DateTimeOffset NewActivity { get; private set; }

        protected override User Given()
        {
            NewActivity = Fixture.Create<DateTimeOffset>();

            return Fixture.Create<User>();
        }

        protected override void When()
        {
            Subject.UpdateLastActivity(NewActivity);
        }

        [Then]
        public void ShouldHaveUpdatedUserLastActivity()
        {
            Subject.LastActivity.ShouldBe(NewActivity);
        }
    }
}