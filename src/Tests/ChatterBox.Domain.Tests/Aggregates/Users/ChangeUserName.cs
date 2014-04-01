using System.Linq;
using Domain.Aggregates.UserAggregate;
using Domain.Aggregates.UserAggregate.Facts;
using Ploeh.AutoFixture;
using Shouldly;

namespace ChatterBox.Domain.Tests.Aggregates.Users
{
    public class WhenChangingUserName : AutoSpecFor<User>
    {
        protected string NewUserName;

        protected override User Given()
        {   
            return Fixture.Create<User>();
        }

        protected override void When()
        {
            NewUserName = Fixture.Create<string>();

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
            var fact = Subject.GetAndClearPendingFacts().OfType<ChangeUserNameFact>().Single();
            
            fact.AggregateRootId.ShouldBe(Subject.Id);
            fact.NewUserName.ShouldBe(NewUserName);
        }
    }
}
