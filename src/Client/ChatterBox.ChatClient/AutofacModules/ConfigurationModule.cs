using Autofac;
using ChatterBox.ChatClient.ConfigurationSettings;
using ConfigInjector.Configuration;

namespace ChatterBox.ChatClient.AutofacModules
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