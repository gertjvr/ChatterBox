using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using ChatterBox.MessageContracts.Messages.Commands;
using Nimbus.MessageContracts;
using Nimbus.Serializers.Json;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoNSubstitute;
using Ploeh.AutoFixture.Idioms;
using Ploeh.AutoFixture.Kernel;
using Shouldly;

namespace ChatterBox.ChatServer.Tests.MessageContracts.Conventions
{
    [TestFixture]
    public class AllMessageContractTypes
    {
        private readonly Func<IFixture> _fixtureFactory;

        public AllMessageContractTypes()
            : this(() => new Fixture().Customize(new AutoNSubstituteCustomization()))
        {
        }

        public AllMessageContractTypes(Func<IFixture> fixtureFactory)
        {
            _fixtureFactory = fixtureFactory;
        }

        public static IEnumerable<Type> GetTypesToVerify()
        {
            return typeof(SendMessageCommand)
                .Assembly
                .GetExportedTypes()
                .Where(IsMessageContractType)
                .Where(t => !t.IsAbstract && !t.IsInterface);
        }

        private static bool IsMessageContractType(Type t)
        {
            if (t.IsClosedTypeOf(typeof(IBusRequest<,>))) return true;
            if (t.IsAssignableTo<IBusEvent>()) return true;
            if (t.IsAssignableTo<IBusResponse>()) return true;
            if (t.IsAssignableTo<IBusCommand>()) return true;
            if (t.IsAssignableTo<IBusEvent>()) return true;

            return false;
        }

        [Test]
        [TestCaseSource("GetTypesToVerify")]
        public void VerifyMessageTypeIsSerializable(Type messageType)
        {
            var fixture = _fixtureFactory();
            var specimenContext = new SpecimenContext(fixture);
            var instance = specimenContext.Resolve(messageType);

            Should.NotThrow(() =>
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(instance);
            });
        }

        [Test]
        [TestCaseSource("GetTypesToVerify")]
        public void VerifyBoundariesForAllPropertiesOnImmutableClass(Type type)
        {
            var assertion = new GuardClauseAssertion(_fixtureFactory());
            PropertyInfo[] properties = type.GetProperties();
            assertion.Verify(properties);
        }

        [Test]
        [TestCaseSource("GetTypesToVerify")]
        public void VerifyBoundariesForAllMethods(Type type)
        {
            var assertion = new GuardClauseAssertion(_fixtureFactory());
            MethodInfo[] methods = type.GetMethods();
            assertion.Verify(methods);
        }

        [Test]
        [TestCaseSource("GetTypesToVerify")]
        public void VerifyBoundariesForAllConstructors(Type type)
        {
            var assertion = new GuardClauseAssertion(_fixtureFactory());
            ConstructorInfo[] ctors = type.GetConstructors();
            assertion.Verify(ctors);
        }

        [Test]
        [TestCaseSource("GetTypesToVerify")]
        public void VerifyConstructorParametersCorrectlyInitializeProperties(Type type)
        {
            var assertion = new ConstructorInitializedMemberAssertion(_fixtureFactory());
            ConstructorInfo[] members = type.GetConstructors();
            assertion.Verify(members);
        }

        [Test]
        [TestCaseSource("GetTypesToVerify")]
        public void VerifyCompositeEqualityBehaviourOnType(Type messageType)
        {
            IFixture fixture = _fixtureFactory();

            var equalityBehaviourAssertion = new CompositeIdiomaticAssertion(
                new EqualsNewObjectAssertion(fixture),
                new EqualsNullAssertion(fixture),
                new EqualsSelfAssertion(fixture),
                new EqualsSuccessiveAssertion(fixture));

            equalityBehaviourAssertion.Verify(messageType);
        }
    }
}