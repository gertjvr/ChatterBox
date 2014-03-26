using Autofac;
using ChatterBox.Core.Infrastructure;
using ChatterBox.Core.Infrastructure.Queries;
using ChatterBox.Core.Persistence;

namespace ChatterBox.ChatServer.AutofacModules
{
    public class QueryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly)
                .AsClosedTypesOf(typeof(IHandleFact<>))
                .AsImplementedInterfaces()
                .OwnedByLifetimeScope();

            builder.RegisterGeneric(typeof(QueryModel<>))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(Repository<>))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}