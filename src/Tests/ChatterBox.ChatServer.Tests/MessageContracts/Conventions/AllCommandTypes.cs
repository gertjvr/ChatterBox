using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using ChatterBox.MessageContracts.Messages.Commands;
using Nimbus.Handlers;
using Nimbus.MessageContracts;
using Shouldly;

namespace ChatterBox.ChatServer.Tests.MessageContracts.Conventions
{
    public class AllCommandTypes
    {
        public void ShouldEndWithCommand(Type commandType)
        {
            commandType.Name.ShouldEndWith("Command");
        }

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

        public static IEnumerable<Type> GetTypesToVerify()
        {
            return typeof (SendMessageCommand).Assembly
                .GetExportedTypes()
                .Where(t => t.IsAssignableTo<IBusCommand>())
                .Where(t => !t.IsAbstract && !t.IsInterface);
        }
    }
}