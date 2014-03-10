using System;
using System.IO;
using Autofac;
using ChatterBox.Server.Configuration;
using ConfigInjector.QuickAndDirty;
using Serilog;
using Serilog.Events;

namespace ChatterBox.Server
{
    public class ChatServer
    {
        private IContainer _container;

        public void Start()
        {
            InitializeLogger();

            _container = IoC.LetThereBeIoC();

            Log.Information(@"Hello, world!");
        }

        private static void InitializeLogger()
        {
            var minimumLogLevel = DefaultSettingsReader.Get<MinimumLogLevelSetting>();

            LoggerConfiguration logConfiguration = new LoggerConfiguration()
                .WriteTo.Trace()
                .WriteTo.RollingFile(
                    Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                        @"ApplicationServerLog-{Date}.txt"))
                .MinimumLevel.Is((LogEventLevel) Enum.Parse(typeof (LogEventLevel), minimumLogLevel));

            if (Environment.UserInteractive)
            {
                logConfiguration.WriteTo.ColoredConsole();
            }

            Log.Logger = logConfiguration.CreateLogger();
        }

        public void Stop()
        {
            Log.Information(@"Goodbye, cruel world!");

            IContainer container = _container;
            if (container != null) container.Dispose();
            _container = null;
        }
    }
}