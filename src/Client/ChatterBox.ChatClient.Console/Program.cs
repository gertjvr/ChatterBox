namespace ChatterBox.ChatClient.Console
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var client = new ChatClient();

            client.Start();

            while (true)
            {
                var message = System.Console.ReadLine();

                if (message == "quit")
                    break;

                client.Send(message).Wait();
            }

            client.Stop();
        }
    }
}
