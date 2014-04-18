using System;
using Autofac.Builder;
using ChatterBox.Core.Tests.IoC.Conventions;

namespace ChatterBox.ChatClient.Tests.IoC.Conventions
{
    public class AllTypesRegisteredWithTheClientContainer : AllTypesRegisteredWithTheContainer
    {
        public AllTypesRegisteredWithTheClientContainer() 
            : base(ChatterBox.ChatClient.IoC.LetThereBeIoC(ContainerBuildOptions.IgnoreStartableComponents))
        {
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