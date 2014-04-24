using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using ChatterBox.MessageContracts.Messages.Commands;
using Nimbus.MessageContracts;
using Nimbus.Serializers.Json;
using NUnit.Framework;
using Ploeh.Albedo;
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
            return typeof (SendMessageCommand)
                .Assembly
                .GetExportedTypes()
                .Where(IsMessageContractType)
                .Where(t => !t.IsAbstract && !t.IsInterface);
        }

        private static bool IsMessageContractType(Type t)
        {
            if (t.IsClosedTypeOf(typeof (IBusRequest<,>))) return true;
            if (t.IsAssignableTo<IBusEvent>()) return true;
            if (t.IsAssignableTo<IBusResponse>()) return true;
            if (t.IsAssignableTo<IBusCommand>()) return true;
            if (t.IsAssignableTo<IBusEvent>()) return true;

            return false;
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
        public void VerifyBoundariesForAllMethods(Type type)
        {
            var assertion = new GuardClauseAssertion(_fixtureFactory());
            MethodInfo[] methods = type.GetMethods();
            assertion.Verify(methods);
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
        public void VerifyMessageTypeIsSerializable(Type messageType)
        {
            IFixture fixture = _fixtureFactory();
            var specimenContext = new SpecimenContext(fixture);
            object instance = specimenContext.Resolve(messageType);

            Should.NotThrow(() =>
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(instance);
            });
        }

        [Test]
        [TestCaseSource("GetTypesToVerify")]
        public void VerifyPublicPropertiesAssignableFromConstructorAreCorrectlyInitialized(Type type)
        {
            var fixture = new Fixture();

            var customMatcher = new VisitorEqualityComparer<NameAndType>(
                new NameAndTypeCollectingVisitor(), new NameAndTypeAssignableComparer());

            var assertion = new ConstructorInitializedMemberAssertion(
                fixture, EqualityComparer<object>.Default, customMatcher);

            PropertyInfo[] properties = type.GetProperties();
            assertion.Verify(properties);
        }

        private class NameAndType
        {
            public NameAndType(string name, Type type)
            {
                Name = name;
                Type = type;
            }

            public string Name { get; private set; }
            public Type Type { get; private set; }
        }

        private class NameAndTypeAssignableComparer : IEqualityComparer<NameAndType>
        {
            public bool Equals(NameAndType x, NameAndType y)
            {
                return string.Equals(x.Name, y.Name, StringComparison.OrdinalIgnoreCase)
                       && (x.Type.IsAssignableFrom(y.Type) || y.Type.IsAssignableFrom(x.Type));
            }

            public int GetHashCode(NameAndType obj)
            {
                return 0;
            }
        }

        private class NameAndTypeCollectingVisitor
            : ReflectionVisitor<IEnumerable<NameAndType>>
        {
            private readonly NameAndType[] values;

            public NameAndTypeCollectingVisitor(
                params NameAndType[] values)
            {
                this.values = values;
            }

            public override IEnumerable<NameAndType> Value
            {
                get { return values; }
            }

            public override IReflectionVisitor<IEnumerable<NameAndType>> Visit(
                FieldInfoElement fieldInfoElement)
            {
                if (fieldInfoElement == null) throw new ArgumentNullException("fieldInfoElement");
                var v = new NameAndType(
                    fieldInfoElement.FieldInfo.Name,
                    fieldInfoElement.FieldInfo.FieldType);
                return new NameAndTypeCollectingVisitor(
                    values.Concat(new[] {v}).ToArray());
            }

            public override IReflectionVisitor<IEnumerable<NameAndType>> Visit(
                ParameterInfoElement parameterInfoElement)
            {
                if (parameterInfoElement == null) throw new ArgumentNullException("parameterInfoElement");
                var v = new NameAndType(
                    parameterInfoElement.ParameterInfo.Name,
                    parameterInfoElement.ParameterInfo.ParameterType);
                return new NameAndTypeCollectingVisitor(
                    values.Concat(new[] {v}).ToArray());
            }

            public override IReflectionVisitor<IEnumerable<NameAndType>> Visit(
                PropertyInfoElement propertyInfoElement)
            {
                if (propertyInfoElement == null) throw new ArgumentNullException("propertyInfoElement");
                var v = new NameAndType(
                    propertyInfoElement.PropertyInfo.Name,
                    propertyInfoElement.PropertyInfo.PropertyType);
                return new NameAndTypeCollectingVisitor(
                    values.Concat(new[] {v}).ToArray());
            }
        }

        private class VisitorEqualityComparer<T> : IEqualityComparer<IReflectionElement>
        {
            private readonly IEqualityComparer<T> comparer;
            private readonly IReflectionVisitor<IEnumerable<T>> visitor;

            internal VisitorEqualityComparer(
                IReflectionVisitor<IEnumerable<T>> visitor,
                IEqualityComparer<T> comparer)
            {
                this.visitor = visitor;
                this.comparer = comparer;
            }

            bool IEqualityComparer<IReflectionElement>.Equals(IReflectionElement x, IReflectionElement y)
            {
                T[] values = new CompositeReflectionElement(x, y)
                    .Accept(visitor)
                    .Value
                    .ToArray();

                IEnumerable<T> distinctValues = values.Distinct(comparer);
                return values.Length == 2
                       && distinctValues.Count() == 1;
            }

            int IEqualityComparer<IReflectionElement>.GetHashCode(IReflectionElement obj)
            {
                if (obj == null) throw new ArgumentNullException("obj");
                return obj
                    .Accept(visitor)
                    .Value
                    .Single()
                    .GetHashCode();
            }
        }
    }
}