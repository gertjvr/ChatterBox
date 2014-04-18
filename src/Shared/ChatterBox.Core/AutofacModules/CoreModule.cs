using Autofac;
using ChatterBox.Core.Infrastructure;

namespace ChatterBox.Core.AutofacModules
{
    public class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<SystemClock>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}