using Autofac;
using ChatterBox.Core.Mapping;

namespace ChatterBox.ChatClient.AutofacModules
{
    public class MappingModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(t => t.IsClosedTypeOf(typeof(IMapToNew<,>)))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(t => t.IsClosedTypeOf(typeof(IMapToExisting<,>)))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}