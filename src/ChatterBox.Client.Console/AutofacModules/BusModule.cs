using System;
using Autofac;
using ChatterBox.Client.Console.Configuration;
using ChatterBox.MessageContracts.Commands;
using Nimbus;
using Nimbus.Configuration;
using Nimbus.Infrastructure;

namespace ChatterBox.Client.Console.AutofacModules
{
    public class BusModule : Module
    {
        internal static Func<string> MachineName = () => Environment.MachineName;

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            var appServerAssembly = ThisAssembly;
            var messageContractsAssembly = typeof (SendMessageCommand).Assembly;

            var handlerTypesProvider = new AssemblyScanningTypeProvider(appServerAssembly, messageContractsAssembly);

            builder.RegisterNimbus(handlerTypesProvider);

            builder.Register(componentContext => new BusBuilder()
                .Configure()
                .WithConnectionString(componentContext.Resolve<NimbusConnectionStringProvider>().ConnectionString)
                .WithNames("ChatterBox.Client.Console", MachineName())
                .WithTypesFrom(handlerTypesProvider)
                .WithAutofacDefaults(componentContext)
                .Build())
                .As<IBus>()
                .AutoActivate()
                .OnActivated(c => c.Instance.Start())
                .SingleInstance();
        }
    }
}