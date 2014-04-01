using System;
using System.IO;
using System.Threading.Tasks;
using Autofac;
using ChatterBox.ChatClient.ConfigurationSettings;
using ChatterBox.Core.ConfigurationSettings;
using ChatterBox.MessageContracts.Messages.Commands;
using ChatterBox.MessageContracts.Users.Commands;
using ChatterBox.MessageContracts.Users.Requests;
using ConfigInjector.QuickAndDirty;
using Nimbus;
using Serilog;
using Serilog.Events;

namespace ChatterBox.ChatClient
{
    public class ChatClient
    {
        private Guid _clientId;
        private Guid _userId;
        private IContainer _container;
        private IBus _bus;

        public async Task<Guid> Connect(string userAgent = null)
        {
            InitializeLogger();

            _container = IoC.LetThereBeIoC();
           
            _userId = Guid.NewGuid();

            _bus = _container.Resolve<IBus>();

            var response = await _bus.Request(new ConnectClientRequest(_userId, userAgent));
            _clientId = response.ClientId;

            Log.Information(@"Hello, world!");

            return _clientId;
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

        public void Disconnect()
        {
            _bus.Send(new DisconnectClientCommand(_clientId));

            Log.Information(@"Goodbye, cruel world!");

            IContainer container = _container;
            if (container != null) container.Dispose();
            _container = null;
        }
        

        public async Task Send(Guid roomId, string content)
        {
            await _bus.Send(new CreateMessageCommand(roomId, _userId, content));
        }
    }
}
