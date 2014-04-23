using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using ChatterBox.Core.Infrastructure.Entities;
using ChatterBox.Core.Infrastructure.Facts;
using ChatterBox.Domain.Aggregates.UserAggregate;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoNSubstitute;
using Ploeh.AutoFixture.Idioms;

namespace ChatterBox.Domain.Tests.Conventions
{
    public class AllDomainTypes
    {
        private readonly Func<IFixture> _fixtureFactory;

        public AllDomainTypes()
            : this(() => new Fixture().Customize(new AutoNSubstituteCustomization()))
        {
        }

        public AllDomainTypes(Func<IFixture> fixtureFactory)
        {
            _fixtureFactory = fixtureFactory;
        }

        public static IEnumerable<Type> GetDomainTypes()
        {
            return typeof (User)
                .Assembly
                .GetExportedTypes()
                .Where(IsDomainType)
                .Where(t => !t.IsAbstract && !t.IsInterface);
        }

        private static bool IsDomainType(Type t)
        {
            if (t.IsAssignableTo<IAggregateRoot>() && !t.IsInterface) return true;
            if (t.IsAssignableTo<IFact>() && !t.IsInterface) return true;

            return false;
        }

        [Test]
        [TestCaseSource("GetDomainTypes")]
        public void VerifyBoundariesForAllPropertiesOnImmutableClass(Type type)
        {
            var assertion = new GuardClauseAssertion(_fixtureFactory());
            var properties = type.GetProperties();
            assertion.Verify(properties);
        }

        [Test]
        [TestCaseSource("GetDomainTypes")]
        public void VerifyBoundariesForAllMethods(Type type)
        {
            var assertion = new GuardClauseAssertion(_fixtureFactory());
            var methods = type.GetMethods();
            assertion.Verify(methods);
        }

        [Test]
        [TestCaseSource("GetDomainTypes")]
        public void VerifyBoundariesForAllConstructors(Type type)
        {
            var assertion = new GuardClauseAssertion(_fixtureFactory());
            var ctors = type.GetConstructors();
            assertion.Verify(ctors);
        }

        [Test]
        [TestCaseSource("GetDomainTypes")]
        public void VerifyConstructorParametersCorrectlyInitializeProperties(Type type)
        {
            var assertion = new ConstructorInitializedMemberAssertion(_fixtureFactory());
            var members = type.GetConstructors();
            assertion.Verify(members);
        }

        [Test]
        [TestCaseSource("GetDomainTypes")]
        public void VerifyCompositeEqualityBehaviourOnManyTypes()
        {
            var fixture = _fixtureFactory();

            var equalityBehaviourAssertion = new CompositeIdiomaticAssertion(
                new EqualsNewObjectAssertion(fixture),
                new EqualsNullAssertion(fixture),
                new EqualsSelfAssertion(fixture),
                new EqualsSuccessiveAssertion(fixture));

            var typesToVerify = typeof(User).Assembly
                .GetExportedTypes()
                .Where(t => t.IsAssignableTo<IAggregateRoot>() || t.IsAssignableTo<IFact>())
                .Where(t => t.IsInterface == false || t.IsAbstract == false);

            equalityBehaviourAssertion.Verify(typesToVerify);
        }
    }
}