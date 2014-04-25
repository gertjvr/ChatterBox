using ChatterBox.Core.Extensions;
using ChatterBox.Core.Tests.Specifications;
using ChatterBox.Domain.Aggregates.UserAggregate;
using Ploeh.AutoFixture;
using Shouldly;

namespace ChatterBox.Domain.Tests.Aggregates.UserAggregate
{
    public class WhenUpdatingUserPassword : AutoSpecificationFor<User>
    {
        public string NewPassword { get; private set; }

        protected override User Given()
        {
            NewPassword = Fixture.Create<string>();

            return Fixture.Create<User>();
        }

        protected override void When()
        {
            Subject.UpdatePassword(NewPassword);
        }

        [Then]
        public void ContainsCorrectPendingFact()
        {
            Subject.HashedPassword.ShouldBe(NewPassword.ToSha256(Subject.Salt));
        }
    }
}