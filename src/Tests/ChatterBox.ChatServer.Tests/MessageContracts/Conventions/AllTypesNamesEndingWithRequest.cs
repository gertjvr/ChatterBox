using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using ChatterBox.MessageContracts.Messages.Commands;
using Nimbus.MessageContracts;
using Shouldly;

namespace ChatterBox.ChatServer.Tests.MessageContracts.Conventions
{
    public class AllTypesNamesEndingWithRequest
    {
        public void ShouldImplementIBusRequest(Type type)
        {
            type.IsClosedTypeOf(typeof (IBusRequest<,>)).ShouldBe(true);
        }

        public static IEnumerable<Type> GetTypesToVerify()
        {
            return typeof (SendMessageCommand).Assembly
                .GetExportedTypes()
                .Where(t => t.Name.EndsWith("Request"))
                .Where(t => !t.IsAbstract);
        }
    }
}