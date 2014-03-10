using System.Diagnostics.CodeAnalysis;
using Autofac;
using Autofac.Builder;

namespace ChatterBox.Client.Console
{
    internal static class IoC
    {
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Io")]
        public static IContainer LetThereBeIoC(ContainerBuildOptions containerBuildOptions = ContainerBuildOptions.None)
        {
            var builder = new ContainerBuilder();

            var appServerAssembly = typeof(IoC).Assembly;
            
            builder.RegisterAssemblyModules(appServerAssembly);

            return builder.Build(containerBuildOptions);
        }
    }
}