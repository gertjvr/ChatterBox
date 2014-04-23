using Autofac;
using ChatterBox.Core.Mapping;

namespace ChatterBox.ChatServer.AutofacModules
{
    public class MappingModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterAssemblyTypes(ThisAssembly)
                .AsClosedTypesOf(typeof (IMapToNew<,>))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            
            builder.RegisterAssemblyTypes(ThisAssembly)
                .AsClosedTypesOf(typeof (IMapToExisting<,>))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}