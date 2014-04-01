using Autofac;
using ChatterBox.Core.Infrastructure;
using ConfigInjector.Configuration;

namespace ChatterBox.ChatServer.AutofacModules
{
    public class ConfigurationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            var coreAssembly = typeof(IClock).Assembly;
            ConfigurationConfigurator.RegisterConfigurationSettings()
                .FromAssemblies(ThisAssembly, coreAssembly)
                .RegisterWithContainer(configSetting => builder.RegisterInstance(configSetting)
                    .AsSelf()
                    .SingleInstance())
                .DoYourThing();
        }
    }
}