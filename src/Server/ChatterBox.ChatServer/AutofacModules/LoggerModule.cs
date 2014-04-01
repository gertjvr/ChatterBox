using System.Threading.Tasks;
using Autofac;
using Nimbus.Logger.Serilog;
using Serilog;

namespace ChatterBox.ChatClient.AutofacModules
{
    public class LoggerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<SerilogStaticLogger>()
                   .AsImplementedInterfaces()
                   .SingleInstance();

            builder.RegisterInstance(Log.Logger)
                   .AsImplementedInterfaces();

            TaskScheduler.UnobservedTaskException += LogUnobservedTaskException;
        }

        private static void LogUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            Log.Logger.Error(e.Exception, "An unobserved exception was thrown on a TaskScheduler thread.");
        }
    }
}