using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using ChatterBox.ChatServer.Handlers.Messages;
using Nimbus.Handlers;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoNSubstitute;
using Ploeh.AutoFixture.Idioms;

namespace ChatterBox.ChatServer.Tests.Handlers
{
    public class AllHandlerTypes
    {
        private readonly Func<IFixture> _fixtureFactory;

        public AllHandlerTypes()
            : this(() => new Fixture().Customize(new AutoNSubstituteCustomization()))
        {
        }

        public AllHandlerTypes(Func<IFixture> fixtureFactory)
        {
            _fixtureFactory = fixtureFactory;
        }

        public static IEnumerable<Type> GetTypesToVerify()
        {
            return typeof(SendMessageCommandHandler)
                .Assembly
                .GetExportedTypes()
                .Where(IsHandlerType)
                .Where(t => !t.IsAbstract && !t.IsInterface);
        }

        private static bool IsHandlerType(Type t)
        {
            if (t.IsClosedTypeOf(typeof(IHandleRequest<,>))) return true;
            if (t.IsClosedTypeOf(typeof(IHandleCompetingEvent<>))) return true;
            if (t.IsClosedTypeOf(typeof(IHandleMulticastEvent<>))) return true;
            if (t.IsClosedTypeOf(typeof(IHandleCommand<>))) return true;

            return false;
        }

        public void VerifyBoundariesForAllMethods(Type type)
        {
            var assertion = new GuardClauseAssertion(_fixtureFactory());
            MethodInfo[] methods = type.GetMethods().Where(info => !info.ReturnType.IsAssignableTo<Task>()).ToArray();
            assertion.Verify(methods);
        }

        public void VerifyBoundariesForAllConstructors(Type type)
        {
            var assertion = new GuardClauseAssertion(_fixtureFactory());
            ConstructorInfo[] ctors = type.GetConstructors();
            assertion.Verify(ctors);
        }
    }
}