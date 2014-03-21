using System;
using System.Diagnostics.CodeAnalysis;
using Autofac;
using Autofac.Builder;

namespace ChatterBox.ChatClient
{
    internal static class IoC
    {
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Io")]
        public static IContainer LetThereBeIoC(ContainerBuildOptions containerBuildOptions = ContainerBuildOptions.None, Action<ContainerBuilder> preHooks = null)
        {
            var builder = new ContainerBuilder();

            var appServerAssembly = typeof(IoC).Assembly;
            
            builder.RegisterAssemblyModules(appServerAssembly);

            if (preHooks != null) preHooks(builder);

            return builder.Build(containerBuildOptions);
        }
    }
}