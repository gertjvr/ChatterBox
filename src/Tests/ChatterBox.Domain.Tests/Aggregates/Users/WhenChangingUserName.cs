using System.Linq;
using ChatterBox.Core.Tests;
using ChatterBox.Core.Tests.Specifications;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.Domain.Aggregates.UserAggregate.Facts;
using Ploeh.AutoFixture;
using Shouldly;

namespace ChatterBox.Domain.Tests.Aggregates.Users
{
    public class WhenChangingUserName : AutoSpecificationFor<User>
    {
        protected string NewUserName;

        protected override User Given()
        {
            NewUserName = Fixture.Create<string>();

            return Fixture.Create<User>();
        }

        protected override void When()
        {
            Subject.UpdateUserName(NewUserName);
        }

        public void NameShouldBeNewUserName()
        {
            Subject.Name.ShouldBe(NewUserName);
        }

        public void ShouldHavePendingFact()
        {
            var pendingFacts = Subject.GetAndClearPendingFacts().OfType<UserNameUpdatedFact>().ToArray();

            pendingFacts.Count().ShouldBe(1);

            var fact = pendingFacts.Single();

            Subject.Id.ShouldBe(fact.AggregateRootId);
            Subject.Name.ShouldBe(fact.NewUserName);
        }
    }
}