using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ChatterBox.Core.Tests;
using ChatterBox.Core.Tests.Specifications;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.Domain.Aggregates.UserAggregate.Facts;
using Ploeh.Albedo;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Idioms;
using Shouldly;

namespace ChatterBox.Domain.Tests.Aggregates.Users
{
    public class WhenCreatingUser : AutoSpecificationFor<User>
    {
        protected override User Given()
        {
            return Fixture.Create<User>();
        }

        protected override void When()
        {
        }

        public void InstanciatedCorrectly()
        {
            Subject.Id.ShouldNotBe(Guid.Empty);
            Subject.Name.ShouldNotBe(string.Empty);
            Subject.Email.ShouldNotBe(string.Empty);
            Subject.Hash.ShouldNotBe(string.Empty);
            Subject.Salt.ShouldNotBe(string.Empty);
            Subject.HashedPassword.ShouldNotBe(string.Empty);
        }

        public void ContainsCorrectPendingFact()
        {
            UserCreatedFact[] pendingFacts = Subject.GetAndClearPendingFacts().OfType<UserCreatedFact>().ToArray();

            pendingFacts.Count().ShouldBe(1);

            UserCreatedFact fact = pendingFacts.Single();

            Subject.Id.ShouldBe(fact.AggregateRootId);
            Subject.Name.ShouldBe(fact.Name);
            Subject.Email.ShouldBe(fact.Email);
            Subject.Hash.ShouldBe(fact.Hash);
            Subject.Salt.ShouldBe(fact.Salt);
            Subject.HashedPassword.ShouldBe(fact.HashedPassword);
        }
    }
}