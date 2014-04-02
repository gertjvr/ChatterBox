using Autofac;
using ChatterBox.Core.Services;

namespace ChatterBox.Core.AutofacModules
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<CryptoService>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
