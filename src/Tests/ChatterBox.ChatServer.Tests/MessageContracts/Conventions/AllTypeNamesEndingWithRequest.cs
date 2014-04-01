﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using ChatterBox.MessageContracts.Messages.Commands;
using Nimbus.MessageContracts;
using NUnit.Framework;
using Shouldly;

namespace ChatterBox.ChatServer.Tests.MessageContracts.Conventions
{
    [TestFixture]
    public class AllTypeNamesEndingWithRequest
    {
        [Test]
        [TestCaseSource(typeof (TestCases))]
        public void ShouldImplementIBusRequest(Type type)
        {
            type.IsClosedTypeOf(typeof (IBusRequest<,>)).ShouldBe(true);
        }

        public class TestCases : IEnumerable<TestCaseData>
        {
            public IEnumerator<TestCaseData> GetEnumerator()
            {
                return typeof (CreateMessageCommand).Assembly
                    .GetExportedTypes()
                    .Where(t => t.Name.EndsWith("Request"))
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