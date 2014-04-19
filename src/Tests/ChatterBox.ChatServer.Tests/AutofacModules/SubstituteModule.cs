using Autofac;
using ChatterBox.Core.Persistence;
using Nimbus;
using NSubstitute;

namespace ChatterBox.ChatServer.Tests.AutofacModules
{
    public class SubstituteModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.Register(context => Substitute.For<IUnitOfWork>())
                .SingleInstance();
            
            builder.Register(context => Substitute.For<IFactStore>())
                .SingleInstance();
            
            builder.Register(context => Substitute.For<IBus>())
                .SingleInstance();
        }
    }
}