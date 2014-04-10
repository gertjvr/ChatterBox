using System.Threading.Tasks;
using Autofac;
using Autofac.Builder;
using ChatterBox.Core.Persistence.Memory;
using Nimbus;
using NUnit.Framework;
using Serilog;
using Serilog.Events;

namespace ChatterBox.ChatServer.IntegrationTests.Scenarios
{
    [Category("IntergrationTests")]
    public abstract class ChatServerSpecificationForBus : SpecificationForAsync<IBus>
    {
        private IContainer _container;

        protected ChatServerSpecificationForBus()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Trace()
                .MinimumLevel.Is(LogEventLevel.Debug)
                .CreateLogger();
        }

        protected virtual IContainer CreateContainer()
        {
            return IoC.LetThereBeIoC(ContainerBuildOptions.None, builder =>
            {
                builder.RegisterType<MemoryFactStore>()
                    .AsImplementedInterfaces()
                    .SingleInstance();
            });
        }

        protected override async Task<IBus> Given()
        {
            _container = CreateContainer();

            return _container.Resolve<IBus>();
        }

        protected override async Task When()
        {
            ((Bus)Subject).Start();
        }

        public async Task TearDown()
        {
            ((Bus)Subject).Stop();
            Subject = null;
        }
    }
}