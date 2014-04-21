using System;
using Autofac.Builder;
using ChatterBox.Core.Tests.IoC;
using ChatterBox.Core.Tests.IoC.Conventions;

namespace ChatterBox.ChatClient.Tests.IoC.Conventions
{
    public class AllTypesRegisteredWithTheClientContainer
    {
        public void VerifyAllTypesCanBeResolved()
        {
            var assertion = new AutofacContainerAssertion(Filter, IsKnownOffender);
            var container = ChatterBox.ChatClient.IoC.LetThereBeIoC(ContainerBuildOptions.IgnoreStartableComponents);
            assertion.Verify(container);
        }

        private bool Filter(Type serviceType)
        {
            return true;
        }

        private bool IsKnownOffender(Type serviceType)
        {
            return false;
        }
    }
}