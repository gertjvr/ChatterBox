using Autofac;
using ChatterBox.Core.Infrastructure;
using ConfigInjector.Configuration;

namespace ChatterBox.ChatClient.AutofacModules
{
    public class ConfigurationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            var coreAssembley = typeof (IClock).Assembly;
            
            ConfigurationConfigurator.RegisterConfigurationSettings()
                .FromAssemblies(ThisAssembly, coreAssembley)
                .RegisterWithContainer(configSetting => builder.RegisterInstance(configSetting)
                    .AsSelf()
                    .SingleInstance())
                .DoYourThing();
        }
    }
}