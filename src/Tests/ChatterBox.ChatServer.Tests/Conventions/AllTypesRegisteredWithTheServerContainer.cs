using System;
using Autofac;
using Autofac.Builder;
using ChatterBox.Core.Infrastructure.Entities;
using NUnit.Framework;

namespace ChatterBox.ChatServer.Tests.Conventions
{
    [TestFixture]
    public class AllTypesRegisteredWithTheServerContainer : AllTypesRegisteredWithTheContainer
    {
        protected override IContainer CreateContainer()
        {
            return IoC.LetThereBeIoC(ContainerBuildOptions.IgnoreStartableComponents);
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