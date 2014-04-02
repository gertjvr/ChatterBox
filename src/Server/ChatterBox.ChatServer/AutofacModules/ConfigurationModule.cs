using Autofac;
using ChatterBox.Core.Infrastructure.Entities;
using ConfigInjector.Configuration;

namespace ChatterBox.ChatServer.AutofacModules
{
    public class ConfigurationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            var coreAssembly = typeof(IAggregateRoot).Assembly;
            ConfigurationConfigurator.RegisterConfigurationSettings()
                .FromAssemblies(ThisAssembly, coreAssembly)
                .RegisterWithContainer(configSetting => builder.RegisterInstance(configSetting)
                    .AsSelf()
                    .SingleInstance())
                .DoYourThing();
        }
    }
}