using Topshelf;

namespace ChatterBox.ChatServer.WindowsService
{
    public class ChatServerHost : ServiceControl
    {
        private ChatServer _chatServer;

        public bool Start(HostControl hostControl)
        {
            _chatServer = new ChatServer();
            _chatServer.Start();
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            var chatServer = _chatServer;
            _chatServer = null;
            if (chatServer == null) return true;
            chatServer.Stop();
            return true;
        }
    }
}