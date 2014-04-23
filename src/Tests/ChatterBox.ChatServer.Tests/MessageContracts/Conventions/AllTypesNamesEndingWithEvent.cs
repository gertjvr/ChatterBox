using System;
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
    public class AllTypesNamesEndingWithEvent
    {
        [Test]
        [TestCaseSource("GetTypesToVerify")]
        public void ShouldImplementIBusEvent(Type type)
        {
            type.IsAssignableTo<IBusEvent>().ShouldBe(true);
        }

        public static IEnumerable<Type> GetTypesToVerify()
        {
            return typeof (SendMessageCommand).Assembly
                .GetExportedTypes()
                .Where(t => t.Name.EndsWith("Event"))
                .Where(t => !t.IsAbstract);
        }
    }
}