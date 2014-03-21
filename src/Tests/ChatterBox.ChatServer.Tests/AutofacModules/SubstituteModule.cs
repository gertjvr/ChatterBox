using Autofac;
using Nimbus;
using NSubstitute;

namespace ChatterBox.ChatServer.Tests.AutofacModules
{
    public class SubstituteModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => Substitute.For<IBus>())
                .As<IBus>()
                .SingleInstance();
        }
    }
}