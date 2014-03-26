using Autofac;
using ChatterBox.Core.Infrastructure;
using ChatterBox.Core.Infrastructure.Entities;
using ChatterBox.Core.Infrastructure.Facts;
using ChatterBox.Core.Persistence;
using ChatterBox.Core.Persistence.Memory;

namespace ChatterBox.Core.AutofacModules
{
    public class PersistenceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MemoryFactStore>()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<AggregateRebuilder>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterType<DomainEventBroker>()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<UnitOfWork>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}