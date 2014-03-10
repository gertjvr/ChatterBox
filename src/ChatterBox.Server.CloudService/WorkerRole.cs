using System.Net;
using System.Threading;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace ChatterBox.Server.CloudService
{
    public class WorkerRole : RoleEntryPoint
    {
        private ChatServer _chatServer;

        public override void Run()
        {
            Thread.Sleep(Timeout.Infinite); // Lazy
        }

        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections 
            ServicePointManager.DefaultConnectionLimit = 12;

            _chatServer = new ChatServer();
            _chatServer.Start();
            
            return base.OnStart();
        }

        public override void OnStop()
        {
            var applicationServer = _chatServer;
            _chatServer = null;
            if (applicationServer == null) return;
            applicationServer.Stop();
        }
    }
}
