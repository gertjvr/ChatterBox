using System;
using Autofac;
using ChatterBox.ChatServer.ConfigurationSettings;
using ChatterBox.MessageContracts.Commands;
using Nimbus;
using Nimbus.Configuration;
using Nimbus.Infrastructure;

namespace ChatterBox.ChatServer.AutofacModules
{
    public class BusModule : Module
    {
        private static readonly Func<string> MachineName = () => Environment.MachineName;
        private static readonly Func<string, string> ConnectionString = cs => cs.Replace("{MachineName}", MachineName());

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            var appServerAssembly = ThisAssembly;
            var messageContractsAssembly = typeof (SendMessageCommand).Assembly;

            var handlerTypesProvider = new AssemblyScanningTypeProvider(appServerAssembly, messageContractsAssembly);

            builder.RegisterNimbus(handlerTypesProvider);
            builder.Register(componentContext => new BusBuilder()
                .Configure()
                .WithConnectionString(ConnectionString(componentContext.Resolve<NimbusConnectionStringSetting>()))
                .WithNames("ChatterBox.ChatServer", MachineName())
                .WithTypesFrom(handlerTypesProvider)
                .WithAutofacDefaults(componentContext)
                .Build())
                .As<IBus>()
                .SingleInstance();
        }
    }
}