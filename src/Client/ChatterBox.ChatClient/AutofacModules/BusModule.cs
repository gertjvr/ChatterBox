using System;
using System.Reflection;
using Autofac;
using ChatterBox.ChatClient.ConfigurationSettings;
using ChatterBox.MessageContracts.Messages.Commands;
using Nimbus;
using Nimbus.Configuration;
using Nimbus.Infrastructure;

namespace ChatterBox.ChatClient.AutofacModules
{
    public class BusModule : Autofac.Module
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
                .WithNames(componentContext.Resolve<ChatClientNameSetting>(), MachineName())
                .WithTypesFrom(handlerTypesProvider)
                .WithAutofacDefaults(componentContext)
                .WithJsonSerializer()
                .Build())
                .As<IBus>()
                .SingleInstance();
        }
    }
}