using Autofac;
using ChatterBox.ChatServer.ConfigurationSettings;
using ChatterBox.Core.Infrastructure;
using ChatterBox.Core.Infrastructure.Queries;
using ChatterBox.Core.Persistence.Disk;

namespace ChatterBox.ChatServer.AutofacModules
{
    public class PersistenceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfWork>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(ThisAssembly)
                .AsClosedTypesOf(typeof(IHandleFact<>))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(Repository<>))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(QueryModel<>))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.Register(c => new DiskFactStore(c.Resolve<FactStoreDirectoryPath>(), c.Resolve<ITypesProvider>()))
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<DomainEventBroker>()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.Register(c => new AssemblyScanningTypesProvider(new [] { ThisAssembly }))
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<AggregateRebuilder>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}