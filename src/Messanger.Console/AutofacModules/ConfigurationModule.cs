using Autofac;
using ChatterBox.Core.Infrastructure.Entities;
using ConfigInjector.Configuration;

namespace Messanger.Console.AutofacModules
{
    public class ConfigurationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var coreAssembly = typeof(AggregateRoot).Assembly;

            ConfigurationConfigurator.RegisterConfigurationSettings()
                         .FromAssemblies(ThisAssembly, coreAssembly)
                         .RegisterWithContainer(configSetting => builder.RegisterInstance(configSetting)
                                                                        .AsSelf()
                                                                        .SingleInstance())
                         .AllowConfigurationEntriesThatDoNotHaveSettingsClasses(true)
                         .DoYourThing();
        }
    }
}