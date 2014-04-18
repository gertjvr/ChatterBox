using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using ChatterBox.MessageContracts.Messages.Commands;
using Nimbus.MessageContracts;
using Shouldly;

namespace ChatterBox.ChatServer.Tests.MessageContracts.Conventions
{
    public class AllTypesNamesEndingWithCommand
    {
        public void ShouldImplementIBusCommand(Type type)
        {
            type.IsAssignableTo<IBusCommand>().ShouldBe(true);
        }

        public static IEnumerable<Type> GetTypesToVerify()
        {
            return typeof (SendMessageCommand).Assembly
                .GetExportedTypes()
                .Where(t => t.Name.EndsWith("Command"))
                .Where(t => !t.IsAbstract);
        }   
    }
}