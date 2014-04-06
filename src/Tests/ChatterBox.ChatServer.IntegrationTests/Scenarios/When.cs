using System.Threading.Tasks;
using Autofac;
using Autofac.Builder;
using ChatterBox.Core.Persistence;
using ChatterBox.Core.Persistence.Memory;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.MessageContracts.Requests;
using Nimbus;
using Serilog;
using Serilog.Events;
using Shouldly;

namespace ChatterBox.ChatServer.IntegrationTests.Scenarios
{
    public class WhenT : SpecFor<IBus>
    {
        private IContainer _container;

        protected override async Task<IBus> Given()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Trace()
                .MinimumLevel.Is(LogEventLevel.Debug)
                .CreateLogger();

            _container = IoC.LetThereBeIoC(ContainerBuildOptions.None, builder =>
            {
                builder.RegisterType<MemoryFactStore>()
                    .AsImplementedInterfaces()
                    .SingleInstance();
            });

            return _container.Resolve<IBus>();
        }

        protected override async Task When()
        {
            ((Bus)Subject).Start();
        }

        public async Task Monkey()
        {
            var response = await Subject.Request(new AuthenticateUserRequest("fred@rockwell.com", "yabadabado"));
            response.User.UserRole.ShouldBe((int)UserRole.Admin);
        }

        public async Task TearDown()
        {
            ((Bus)Subject).Stop();
            Subject = null;
        }
    }
}