using System.Linq;
using ChatterBox.Core.Extensions;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.Domain.Aggregates.UserAggregate.Facts;
using Ploeh.AutoFixture;
using Shouldly;

namespace ChatterBox.Domain.Tests.Aggregates.Users
{
    public class WhenChangingUserPassword : AutoSpecificationFor<User>
    {
        protected string NewPassword;
        protected string NewHashedPassword;

        protected override User Given()
        {
            var user = Fixture.Create<User>();

            NewPassword = Fixture.Create<string>();
            NewHashedPassword = NewPassword.ToSha256(user.Salt);

            return user;
        }

        protected override void When()
        {
            Subject.ChangePassword(NewHashedPassword);
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