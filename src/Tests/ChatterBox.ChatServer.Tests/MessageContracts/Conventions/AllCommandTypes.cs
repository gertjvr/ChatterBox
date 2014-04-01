using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using ChatterBox.MessageContracts.Messages.Commands;
using Nimbus.Handlers;
using Nimbus.MessageContracts;
using NUnit.Framework;
using Shouldly;

namespace ChatterBox.ChatServer.Tests.MessageContracts.Conventions
{
    [TestFixture]
    public class AllCommandTypes
    {
        [Test]
        [TestCaseSource(typeof (TestCases))]
        public void ShouldEndWithCommand(Type commandType)
        {
            commandType.Name.ShouldEndWith("Command");
        }

        [Test]
        [TestCaseSource(typeof (TestCases))]
        public void ShouldHaveAtLeastOneHandler(Type commandType)
        {
            var handlerType = typeof (IHandleCommand<>).MakeGenericType(commandType);

            var relevantHandlers = new[] { typeof (ChatServer) }
                .Select(t => t.Assembly)
                .SelectMany(a => a.GetExportedTypes())
                .Where(handlerType.IsAssignableFrom)
                .ToArray();

            relevantHandlers.ShouldNotBeEmpty();
        }

        internal class TestCases : IEnumerable<TestCaseData>
        {
            public IEnumerator<TestCaseData> GetEnumerator()
            {
                return typeof (CreateMessageCommand)
                    .Assembly
                    .GetExportedTypes()
                    .Where(t => t.IsAssignableTo<IBusCommand>())
                    .Select(t => new TestCaseData(t)
                                .SetName(t.FullName))
                    .GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }
}