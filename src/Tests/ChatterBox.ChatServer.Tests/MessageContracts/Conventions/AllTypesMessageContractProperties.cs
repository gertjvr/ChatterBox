using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using ChatterBox.MessageContracts.Messages.Commands;
using Nimbus.MessageContracts;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoNSubstitute;

namespace ChatterBox.ChatServer.Tests.MessageContracts.Conventions
{
    public class AllTypesMessageContractProperties
    {
        private readonly Func<IFixture> _fixtureFactory;

        public AllTypesMessageContractProperties()
            : this(() => new Fixture().Customize(new AutoNSubstituteCustomization()))
        {
        }

        public AllTypesMessageContractProperties(Func<IFixture> fixtureFactory)
        {
            _fixtureFactory = fixtureFactory;
        }

        public static IEnumerable<PropertyInfo> GetPropertiesToVerify()
        {
            return typeof(SendMessageCommand)
                .Assembly
                .GetExportedTypes()
                .Where(IsMessageContractTypes)
                .SelectMany(t => t.GetProperties());
        }

        private static bool IsMessageContractTypes(Type t)
        {
            if (t.IsClosedTypeOf(typeof(IBusRequest<,>))) return true;
            if (t.IsAssignableTo<IBusResponse>() && !t.IsInterface) return true;
            if (t.IsAssignableTo<IBusCommand>() && !t.IsInterface) return true;
            if (t.IsAssignableTo<IBusEvent>() && !t.IsInterface) return true;

            return false;
        }
    }
}