using System;
using Autofac;
using Autofac.Builder;
using ChatterBox.Core.Infrastructure.Entities;
using ChatterBox.Core.Tests.IoC.Conventions;

namespace ChatterBox.ChatServer.Tests.IoC.Conventions
{
    public class AllTypesRegisteredWithTheServerContainer : AllTypesRegisteredWithTheContainer
    {
        public AllTypesRegisteredWithTheServerContainer()
            : base(ChatterBox.ChatServer.IoC.LetThereBeIoC(ContainerBuildOptions.IgnoreStartableComponents))
        {
        }

        protected override bool Filter(Type serviceType)
        {
            if (serviceType.IsAssignableTo<IAggregateRoot>()) return false;

            return true;
        }

        protected override bool IsKnownOffender(Type serviceType)
        {
            //if (serviceType == typeof(IReportingDataService)) return true;

            return false;
        }
    }
}