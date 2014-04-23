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
    public class AllTypesNamesEndingWithResponse
    {
        [Test]
        [TestCaseSource("GetTypesToVerify")]
        public void ShouldImplementIBusResponse(Type type)
        {
            type.IsAssignableTo<IBusResponse>().ShouldBe(true);
        }

        public static IEnumerable<Type> GetTypesToVerify()
        {
            return typeof (SendMessageCommand).Assembly
                .GetExportedTypes()
                .Where(t => t.Name.EndsWith("Response"))
                .Where(t => !t.IsAbstract);
        }
    }
}