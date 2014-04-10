using System;
using ChatterBox.ChatServer.ConfigurationSettings;
using ConfigInjector.QuickAndDirty;
using Nimbus;
using Nimbus.Configuration;
using Nimbus.Infrastructure;

namespace ChatterBox.ChatServer.IntegrationTests
{
    public static class TestHarnessBusFactory
    {
        private static readonly Func<string> MachineName = () => Environment.MachineName;
        private static readonly Func<string, string> ConnectionString = cs => cs.Replace("{MachineName}", MachineName());

        public static Bus Create(ITypeProvider typeProvider, DefaultMessageHandlerFactory messageHandlerFactory, ILogger logger)
        {
            var connectionString = DefaultSettingsReader.Get<NimbusConnectionStringSetting>();

            var bus = new BusBuilder()
                .Configure()
                .WithNames("ChatterBox.MyTestSuite", Environment.MachineName)
                .WithConnectionString(ConnectionString(connectionString))
                .WithTypesFrom(typeProvider)
                .WithDefaultHandlerFactory(messageHandlerFactory)
                .WithDefaultTimeout(TimeSpan.FromSeconds(10))
                .WithLogger(logger)
                .WithDebugOptions(
                    dc =>
                        dc.RemoveAllExistingNamespaceElementsOnStartup(
                            "I understand this will delete EVERYTHING in my namespace. I promise to only use this for test suites."))
                .Build();
            return bus;
        }
    }
}