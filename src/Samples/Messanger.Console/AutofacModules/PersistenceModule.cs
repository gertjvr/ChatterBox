using Autofac;
using ChatterBox.ChatServer.ConfigurationSettings;
using ChatterBox.Core.Infrastructure;
using ChatterBox.Core.Infrastructure.Queries;
using ChatterBox.Core.Persistence.Disk;

namespace Messanger.Console.AutofacModules
{
    public class PersistenceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
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

            builder.RegisterType<UnitOfWork>()
                .AsImplementedInterfaces()
                .InstancePerDependency();

            builder.RegisterAssemblyTypes(ThisAssembly)
                .AsClosedTypesOf(typeof(IHandleFact<>))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(Repository<>))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(QueryModel<>))
                .AsImplementedInterfaces()
                .SingleInstance();
            
        }
    }
}