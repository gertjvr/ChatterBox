using System;
using Autofac;
using Autofac.Builder;
using ChatterBox.Server;
using ChatterBox.Tests.Conventions;
using NUnit.Framework;

namespace ChatterBox.Tests.Server.Conventions
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
            return true;
        }

        protected override bool IsKnownOffender(Type serviceType)
        {
            //if (serviceType == typeof(IReportingDataService)) return true;

            return false;
        }
    }
}