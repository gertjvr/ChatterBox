
namespace ChatterBox.ChatClient.Console
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var client = new ChatClient("Console-Agent");

            var logOnInfo = client.Register("fred", "fred@rockwell.com", "yabadabado").Result;
            var theWorldRoomId = client.CreateRoom("TheWorld").Result;

            while (true)
            {
                var command = System.Console.ReadLine();
                var parameters = command.Split(' ');

                if (parameters[0] == "send")
                {
                    client.Send(parameters[1], theWorldRoomId).Wait();
                }

                if (parameters[0] == "room")
                {
                    var room = client.GetRoomInfo(theWorldRoomId).Result;
                    foreach (var message in room.RecentMessages)
                    {
                        System.Console.WriteLine("[{0}][{1}] {2}", message.CreatedAt, message.User.Name, message.Content);
                    }
                }

                if (parameters[0] == "quit")
                    break;
            }

            client.Disconnect();
        }
    }
}
