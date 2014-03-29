using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Autofac;
using ChatterBox.MessageContracts.Commands;
using Nimbus.MessageContracts;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoNSubstitute;
using Ploeh.AutoFixture.Kernel;
using Shouldly;
using ThirdDrawer.Extensions.CollectionExtensionMethods;

namespace ChatterBox.Shared.Tests.MessageContracts
{
    [TestFixture]
    public class AllMessageContractTypes
    {
        [Test]
        [TestCaseSource(typeof (TestCases))]
        public void ShouldHaveADefaultConstructor(Type requestType)
        {
            var defaultConstructor = requestType
                .GetConstructors(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
                .Where(c => c.GetParameters().None())
                .FirstOrDefault();
            defaultConstructor.ShouldNotBe(null);
        }

        [Test]
        [TestCaseSource(typeof (TestCases))]
        public void ShouldHaveAppropriateConstructors(Type requestType)
        {
            var constructors = requestType.GetConstructors(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

            // only one constructor? it should be public.
            if (constructors.Count() == 1)
            {
                constructors.Single().IsPublic.ShouldBe(true);
                return;
            }

            // more than one constructor? the default constructor should be protected and the others public.
            var defaultConstructor = constructors.Where(c => c.GetParameters().None()).FirstOrDefault();
            defaultConstructor.ShouldNotBe(null);
            defaultConstructor.IsFamily.ShouldBe(true);

            var nonDefaultConstructors = constructors.Where(c => c.GetParameters().Any());
            foreach (var nonDefaultConstructor in nonDefaultConstructors)
            {
                nonDefaultConstructor.IsPublic.ShouldBe(true);
            }
        }

        [Test]
        [TestCaseSource(typeof(TestCases))]
        public void ShouldBeSerializable(Type messageType)
        {
            Should.NotThrow(() =>
            {
                using (var mem = new MemoryStream())
                {
                    var serializer = new DataContractSerializer(messageType);
                    var context = new SpecimenContext(new Fixture().Customize(new AutoNSubstituteCustomization()));
                    var instance = context.Resolve(messageType);
                    serializer.WriteObject(mem, instance);
                }
            });
        }

        internal class TestCases : IEnumerable<TestCaseData>
        {
            public IEnumerator<TestCaseData> GetEnumerator()
            {
                return typeof (BroadcastMessageCommand)
                    .Assembly
                    .GetExportedTypes()
                    .Where(IsRequestOrResponseOrCommandOrEventType)
                    .Select(t => new TestCaseData(t)
                                .SetName(t.FullName))
                    .GetEnumerator();
            }

            private static bool IsRequestOrResponseOrCommandOrEventType(Type t)
            {
                if (t.IsClosedTypeOf(typeof (IBusRequest<,>))) return true;
                if (t.IsAssignableTo<IBusResponse>()) return true;
                if (t.IsAssignableTo<IBusCommand>()) return true;
                if (t.IsAssignableTo<IBusEvent>()) return true;

                return false;
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }
}