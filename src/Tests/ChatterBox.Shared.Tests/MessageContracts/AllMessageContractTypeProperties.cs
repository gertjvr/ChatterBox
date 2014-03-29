using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using ChatterBox.MessageContracts.Commands;
using Nimbus.MessageContracts;
using NUnit.Framework;
using Shouldly;

namespace ChatterBox.Shared.Tests.MessageContracts
{
    [TestFixture]
    public class AllMessageContractTypeProperties
    {
        [Test]
        [TestCaseSource(typeof(TestCases))]
        public void ShouldBePublic(PropertyInfo propertyInfo)
        {
            propertyInfo.GetMethod.IsPublic.ShouldBe(true);
            propertyInfo.SetMethod.IsPublic.ShouldBe(true);
        }

        internal class TestCases : IEnumerable<TestCaseData>
        {
            public IEnumerator<TestCaseData> GetEnumerator()
            {
                return typeof(BroadcastMessageCommand)
                    .Assembly
                    .GetExportedTypes()
                    .Where(IsRequestOrResponseOrCommandOrEventType)
                    .SelectMany(t => t.GetProperties())
                    .Select(p => new TestCaseData(p)
                                .SetName(p.DeclaringType.FullName + "." + p.Name))
                    .GetEnumerator();
            }

            private static bool IsRequestOrResponseOrCommandOrEventType(Type t)
            {
                if (t.IsClosedTypeOf(typeof(IBusRequest<,>))) return true;
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