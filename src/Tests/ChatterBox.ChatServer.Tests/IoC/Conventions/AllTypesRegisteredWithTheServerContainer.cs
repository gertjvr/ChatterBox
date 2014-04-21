using System;
using Autofac;
using Autofac.Builder;
using ChatterBox.ChatServer.Tests.AutofacModules;
using ChatterBox.Core.Infrastructure.Entities;
using ChatterBox.Core.Tests.IoC.Conventions;

namespace ChatterBox.ChatServer.Tests.IoC.Conventions
{
    public class AllTypesRegisteredWithTheServerContainer
    {
        public void VerifyAllTypesCanBeResolved()
        {
            var assertion = new AutofacContainerAssertion(Filter, IsKnownOffender);
            var container = ChatterBox.ChatServer.IoC.LetThereBeIoC(ContainerBuildOptions.IgnoreStartableComponents, builder => builder.RegisterModule<SubstituteModule>());
            assertion.Verify(container);
            container.Dispose();
        }

        private bool Filter(Type serviceType)
        {
            if (serviceType.IsAssignableTo<IAggregateRoot>()) return false;

            return true;
        }

        private bool IsKnownOffender(Type serviceType)
        {
            //if (serviceType == typeof(IReportingDataService)) return true;

            return false;
        }
    }
}