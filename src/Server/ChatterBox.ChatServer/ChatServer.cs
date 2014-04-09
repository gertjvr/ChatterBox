using System;
using System.IO;
using System.Threading.Tasks;
using Autofac;
using ChatterBox.ChatClient.ConfigurationSettings;
using ChatterBox.ChatServer.ConfigurationSettings;
using ConfigInjector.QuickAndDirty;
using Nimbus;
using Seq;
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
            var serverUrl = DefaultSettingsReader.Get<SeqServerUriSetting>();
            var logPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), @"ClientConsoleLog-{Date}.txt");

            LoggerConfiguration logConfiguration = new LoggerConfiguration()
                .MinimumLevel.Is((LogEventLevel)Enum.Parse(typeof(LogEventLevel), minimumLogLevel))
                .WriteTo.Trace()
                .WriteTo.RollingFile(logPath)
                .WriteTo.Seq(serverUrl);

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