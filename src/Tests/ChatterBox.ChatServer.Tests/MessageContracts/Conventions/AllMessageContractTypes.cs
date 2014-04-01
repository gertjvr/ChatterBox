using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Autofac;
using ChatterBox.MessageContracts.Messages.Commands;
using Nimbus.MessageContracts;
using NUnit.Framework;
using Shouldly;
using ThirdDrawer.Extensions.CollectionExtensionMethods;

namespace ChatterBox.ChatServer.Tests.MessageContracts.Conventions
{
    [TestFixture]
    public class AllMessageContractTypes
    {
        [Test]
        [TestCaseSource(typeof (TestCases))]
        public void ShouldBeSerializable(Type messageType)
        {
            Should.NotThrow(() =>
            {
                using (var mem = new MemoryStream())
                {
                    var serializer = new DataContractSerializer(messageType);
                    object instance = Activator.CreateInstance(messageType, true);
                    serializer.WriteObject(mem, instance);
                }
            });

            //ShouldNotThrow("The message type {0} is not serializable.".FormatWith(messageType.FullName));
        }

        internal class TestCases : IEnumerable<TestCaseData>
        {
            public IEnumerator<TestCaseData> GetEnumerator()
            {
                return typeof (CreateMessageCommand)
                    .Assembly
                    .GetExportedTypes()
                    .Where(IsRequestOrResponseOrCommandOrEventType)
                    .Select(t => new TestCaseData(t)
                        .SetName(t.FullName))
                    .GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            private static bool IsRequestOrResponseOrCommandOrEventType(Type t)
            {
                if (t.IsClosedTypeOf(typeof (IBusRequest<,>))) return true;
                if (t.IsAssignableTo<IBusEvent>()) return true;
                if (t.IsAssignableTo<IBusResponse>()) return true;
                if (t.IsAssignableTo<IBusCommand>()) return true;
                if (t.IsAssignableTo<IBusEvent>()) return true;

                return false;
            }
        }

        [Test]
        [TestCaseSource(typeof (TestCases))]
        public void ShouldHaveADefaultConstructor(Type messageType)
        {
            ConstructorInfo defaultConstructor = messageType
                .GetConstructors(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
                .FirstOrDefault(c => c.GetParameters().Any() == false);
            defaultConstructor.ShouldNotBe(null);
        }

        [Test]
        [TestCaseSource(typeof (TestCases))]
        public void ShouldHaveAppropriateConstructors(Type messageType)
        {
            ConstructorInfo[] constructors =
                messageType.GetConstructors(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

            // only one constructor? it should be public.
            if (constructors.Count() == 1)
            {
                constructors.Single().IsPublic.ShouldBe(true);
                return;
            }

            // more than one constructor? the default constructor should be protected and the others public.
            ConstructorInfo defaultConstructor = constructors.FirstOrDefault(c => c.GetParameters().Any() == false);
            defaultConstructor.ShouldNotBe(null);
            defaultConstructor.IsFamily.ShouldBe(true);

            IEnumerable<ConstructorInfo> nonDefaultConstructors = constructors.Where(c => c.GetParameters().Any() == false);
            foreach (ConstructorInfo nonDefaultConstructor in nonDefaultConstructors)
            {
                nonDefaultConstructor.IsPublic.ShouldBe(false);
            }
        }
    }
}