﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using ChatterBox.MessageContracts.Commands;
using Nimbus.MessageContracts;
using NUnit.Framework;
using Shouldly;

namespace ChatterBox.ChatServer.Tests.MessageContracts.Conventions
{
    [TestFixture]
    public class AllTypeNamesEndingWithEvent
    {
        [Test]
        [TestCaseSource(typeof (TestCases))]
        public void ShouldImplementIBusResponse(Type type)
        {
            type.IsAssignableTo<IBusEvent>().ShouldBe(true);
        }

        public class TestCases : IEnumerable<TestCaseData>
        {
            public IEnumerator<TestCaseData> GetEnumerator()
            {
                return typeof (SendMessageCommand).Assembly
                    .GetExportedTypes()
                    .Where(t => t.Name.EndsWith("Event"))
                    .Where(t => !t.IsAbstract)
                    .Select(t => new TestCaseData(t)
                        .SetName(t.FullName)
                    ).GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }
}