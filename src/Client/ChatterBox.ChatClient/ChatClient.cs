using System;
using System.IO;
using System.Threading.Tasks;
using Autofac;
using ChatterBox.ChatClient.Configuration;
using ChatterBox.MessageContracts.Commands;
using ConfigInjector.QuickAndDirty;
using Nimbus;
using Serilog;
using Serilog.Events;

namespace ChatterBox.ChatClient
{
    public class ChatClient
    {
        private Guid _clientId;
        private IContainer _container;
        private IBus _bus;

        public void Start(Guid? clientId = null)
        {
            InitializeLogger();

            _container = IoC.LetThereBeIoC();

            _clientId = clientId ?? Guid.NewGuid();
            _bus = _container.Resolve<IBus>();

            Log.Information(@"Hello, world!");
        }

        private static void InitializeLogger()
        {
            var minimumLogLevel = DefaultSettingsReader.Get<MinimumLogLevelSetting>();

            LoggerConfiguration logConfiguration = new LoggerConfiguration()
                .WriteTo.Trace()
                .WriteTo.RollingFile(
                    Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                        @"ClientConsoleLog-{Date}.txt"))
                .MinimumLevel.Is((LogEventLevel)Enum.Parse(typeof(LogEventLevel), minimumLogLevel));

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
        

        public async Task Send(string message)
        {
            await _bus.Send(new BroadcastMessageCommand(_clientId, message));
        }
    }
}
