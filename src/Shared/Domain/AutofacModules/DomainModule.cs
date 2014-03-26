using Autofac;
using ChatterBox.Core.Infrastructure.Entities;
using ChatterBox.Core.Infrastructure.Facts;

namespace Domain.AutofacModules
{
    public class DomainModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(t => t.IsAssignableTo<IAggregateRoot>())
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(t => t.IsAssignableTo<IFact>())
                .InstancePerLifetimeScope();
        }
    }
}