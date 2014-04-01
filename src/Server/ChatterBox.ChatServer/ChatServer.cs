using System;
using System.IO;
using System.Threading.Tasks;
using Autofac;
using ChatterBox.Core.ConfigurationSettings;
using ConfigInjector.QuickAndDirty;
using Nimbus;
using Serilog;
using Serilog.Events;

namespace ChatterBox.ChatServer
{
    public class ChatServer
    {
        private IContainer _container;

        public void Start()
        {
            InitializeLogger();

            _container = IoC.LetThereBeIoC();

            StartNimbus((Bus) _container.Resolve<IBus>());

            Log.Information(@"Hello, world!");
        }

        private void StartNimbus(Bus bus)
        {
            bus.Start();
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

            TaskScheduler.UnobservedTaskException += (sender, args) => Log.Logger.Error(args.Exception, "An unobserved exception was thrown on a TaskScheduler thread.");
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