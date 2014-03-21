using Topshelf;

namespace ChatterBox.ChatServer.WindowsService
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            HostFactory.Run(svc =>
            {
                svc.Service<ChatServerHost>();
                svc.RunAsNetworkService();
                svc.SetServiceName("ChatterBoxChatServer");
                svc.SetDisplayName("ChatterBox ChatServer");
            });
        }
    }
}