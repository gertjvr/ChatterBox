using System;
using System.Linq;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.Domain.Aggregates.UserAggregate.Facts;
using Ploeh.AutoFixture;
using Shouldly;

namespace ChatterBox.Domain.Tests.Aggregates.Users
{
    public class WhenCreatingUser : AutoSpecFor<User>
    {
        protected override User Given()
        {
            return Fixture.Create<User>();
        }

        protected override void When()
        {   
        }

        [Then]
        public void InstanciatedCorrectly()
        {
            Subject.Id.ShouldNotBe(Guid.Empty);
            Subject.Name.ShouldNotBe(string.Empty);
            Subject.Email.ShouldNotBe(string.Empty);
            Subject.Hash.ShouldNotBe(string.Empty);
            Subject.Salt.ShouldNotBe(string.Empty);
            Subject.HashedPassword.ShouldNotBe(string.Empty);
        }

        [Then]
        public void ContainsCorrectPendingFact()
        {
            var pendingFacts = Subject.GetAndClearPendingFacts().OfType<UserCreatedFact>().ToArray();
            
            pendingFacts.Count().ShouldBe(1);

            var fact = pendingFacts.Single();

            Subject.Id.ShouldBe(fact.AggregateRootId);
            Subject.Name.ShouldBe(fact.Name);
            Subject.Email.ShouldBe(fact.Email);
            Subject.Hash.ShouldBe(fact.Hash);
            Subject.Salt.ShouldBe(fact.Salt);
            Subject.HashedPassword.ShouldBe(fact.HashedPassword);
        }
    }
}