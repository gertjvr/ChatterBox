using System;
using Autofac;
using Autofac.Builder;
using ChatterBox.ChatClient.Tests.AutofacModules;
using ChatterBox.Core.Tests.IoC.Conventions;
using NUnit.Framework;

namespace ChatterBox.ChatClient.Tests.IoC.Conventions
{
    [TestFixture]
    public class AllTypesRegisteredWithTheClientContainer
    {
        [Test]
        public void VerifyAllTypesCanBeResolved()
        {
            var assertion = new AutofacContainerAssertion(Filter, IsKnownOffender);
            var container = ChatterBox.ChatClient.IoC.LetThereBeIoC(ContainerBuildOptions.IgnoreStartableComponents);
            assertion.Verify(container);
        }

        [Test]
        public void VerifyAllRegisteredTypesLifetimes()
        {
            var assertion = new AutofacLifetimeAssertion();
            var container = ChatterBox.ChatClient.IoC.LetThereBeIoC(ContainerBuildOptions.IgnoreStartableComponents, builder => builder.RegisterModule<SubstituteModule>());
            assertion.Verify(container);
            container.Dispose();
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