using System;
using System.Reflection;
using Autofac;
using ChatterBox.ChatClient.ConfigurationSettings;
using ChatterBox.MessageContracts.Commands;
using Nimbus;
using Nimbus.Configuration;
using Nimbus.Infrastructure;

namespace ChatterBox.ChatClient.AutofacModules
{
    public class BusModule : Autofac.Module
    {
        internal static Func<string> MachineName = () => Environment.MachineName;
        internal static Func<string, string> ConnectionString = cs => cs.Replace("{MachineName}", MachineName()); 

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            Assembly appServerAssembly = ThisAssembly;
            Assembly messageContractsAssembly = typeof (SendMessageCommand).Assembly;

            var handlerTypesProvider = new AssemblyScanningTypeProvider(appServerAssembly, messageContractsAssembly);

            builder.RegisterNimbus(handlerTypesProvider);

            builder.Register(componentContext => new BusBuilder()
                .Configure()
                .WithConnectionString(ConnectionString(componentContext.Resolve<NimbusConnectionStringSetting>()))
                .WithNames(componentContext.Resolve<ChatClientNameSetting>(), MachineName())
                .WithTypesFrom(handlerTypesProvider)
                .WithAutofacDefaults(componentContext)
                .Build())
                .As<IBus>()
                .SingleInstance();
        }
    }
}