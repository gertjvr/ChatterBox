using Autofac;
using ChatterBox.Server.Configuration;
using ConfigInjector.Configuration;

namespace ChatterBox.Server.AutofacModules
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