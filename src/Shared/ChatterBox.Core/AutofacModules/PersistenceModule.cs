﻿using Autofac;
using ChatterBox.Core.ConfigurationSettings;
using ChatterBox.Core.Infrastructure;
using ChatterBox.Core.Persistence;
using ChatterBox.Core.Persistence.Disk;

namespace ChatterBox.Core.AutofacModules
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
                .InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}