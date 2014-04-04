using System;

namespace ChatterBox.ChatClient.Console
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var client = new ChatClient();

            var logOnInfo = client.Connect("fred@rockwell.com", "yabadabado").Result;

            while (true)
            {
                var message = System.Console.ReadLine();

                if (message == "quit")
                    break;

                client.Send(message, Guid.NewGuid()).Wait();
            }

            client.Disconnect();
        }
    }
}
