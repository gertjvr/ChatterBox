using System.Linq;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.Domain.Aggregates.UserAggregate.Facts;
using Ploeh.AutoFixture;
using Shouldly;
using SpecificationFor;

namespace ChatterBox.Domain.Tests.Aggregates.Users
{
    public class WhenChangingUserName : AutoSpecFor<User>
    {
        protected string NewUserName;

        protected override User Given()
        {
            NewUserName = Fixture.Create<string>();

            return Fixture.Create<User>();
        }

        protected override void When()
        {
            Subject.ChangeUserName(NewUserName);
        }

        [Then]
        public void NameShouldBeNewUserName()
        {
            Subject.Name.ShouldBe(NewUserName);
        }

        [Then]
        public void ShouldHavePendingFact()
        {
            var pendingFacts = Subject.GetAndClearPendingFacts().OfType<UserNameChangedFact>().ToArray();

            pendingFacts.Count().ShouldBe(1);

            var fact = pendingFacts.Single();

            Subject.Id.ShouldBe(fact.AggregateRootId);
            Subject.Name.ShouldBe(fact.NewUserName);
        }
    }
}