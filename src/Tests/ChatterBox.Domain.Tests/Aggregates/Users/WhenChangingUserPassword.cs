using System.Linq;
using ChatterBox.Core.Extentions;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.Domain.Aggregates.UserAggregate.Facts;
using Ploeh.AutoFixture;
using Shouldly;

namespace ChatterBox.Domain.Tests.Aggregates.Users
{
    public class WhenChangingUserPassword : AutoSpecFor<User>
    {
        protected string NewPassword;
        protected string NewHashedPassword;

        protected override User Given()
        {
            NewPassword = Fixture.Create<string>();
            NewHashedPassword = NewPassword.ToSha256(Subject.Salt);

            return Fixture.Create<User>();
        }

        protected override void When()
        {
            Subject.SetUserPassword(NewPassword);
        }

        [Then]
        public void ChangedPasswordCorrectly()
        {
            Subject.HashedPassword.ShouldBe(NewHashedPassword);
        }

        [Then]
        public void ContainsCorrectPendingFact()
        {
            var pendingFacts = Subject.GetAndClearPendingFacts().OfType<UserPasswordChangedFact>().ToArray();

            pendingFacts.Count().ShouldBe(1);

            var fact = pendingFacts.Single();

            Subject.Id.ShouldBe(fact.AggregateRootId);
            Subject.HashedPassword.ShouldBe(fact.NewHashedPassword);
        }
    }
}