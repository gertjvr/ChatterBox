using System;
using System.Diagnostics.CodeAnalysis;
using Autofac;
using Autofac.Builder;
using ChatterBox.Core.Infrastructure;
using ChatterBox.Core.Infrastructure.Entities;
using ChatterBox.Domain.AutofacModules;

namespace ChatterBox.ChatServer
{
    internal static class IoC
    {
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Io")]
        public static IContainer LetThereBeIoC(ContainerBuildOptions containerBuildOptions = ContainerBuildOptions.None, Action<ContainerBuilder> preHooks = null)
        {
            var builder = new ContainerBuilder();

            var thisAssembly = typeof(IoC).Assembly;
            var coreAssembly = typeof(IAggregateRoot).Assembly;
            var domainAssembly = typeof(DomainModule).Assembly;

            builder.RegisterAssemblyModules(thisAssembly, coreAssembly, domainAssembly);

            if (preHooks != null) preHooks(builder);

            return builder.Build(containerBuildOptions);
        }
    }
}