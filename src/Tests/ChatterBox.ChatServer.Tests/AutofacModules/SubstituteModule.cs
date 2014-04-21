using Autofac;
using ChatterBox.Core.Persistence.Memory;
using Nimbus;
using NSubstitute;

namespace ChatterBox.ChatServer.Tests.AutofacModules
{
    public class SubstituteModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<MemoryFactStore>()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.Register(context => Substitute.For<IBus>())
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}