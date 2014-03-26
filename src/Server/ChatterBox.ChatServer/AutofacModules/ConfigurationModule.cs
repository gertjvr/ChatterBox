using Autofac;
using ChatterBox.ChatServer.ConfigurationSettings;
using ConfigInjector.Configuration;

namespace ChatterBox.ChatServer.AutofacModules
{
    public class ConfigurationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            ConfigurationConfigurator.RegisterConfigurationSettings()
                .FromAssemblies(ThisAssembly)
                .RegisterWithContainer(configSetting => builder.RegisterInstance(configSetting)
                    .AsSelf()
                    .SingleInstance())
                .DoYourThing();

            builder.RegisterType<NimbusConnectionStringProvider>()
                .AsSelf()
                .SingleInstance();
        }
    }
}