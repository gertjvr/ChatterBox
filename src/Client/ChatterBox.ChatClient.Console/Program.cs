
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThirdDrawer.Extensions.StringExtensionMethods;

namespace ChatterBox.ChatClient.Console
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var client = new ChatClient("Console-Agent");
            var availibleRooms = new Dictionary<string, Guid>();

            System.Console.WriteLine("*** Welcome to ChatterBox ***");
            System.Console.WriteLine("1. Register");
            System.Console.WriteLine("2. Connect");
            System.Console.WriteLine("3. Create Room");
            System.Console.WriteLine("4. Join Room");
            System.Console.WriteLine("5. List Rooms");
            System.Console.WriteLine("6. Send Message");
            System.Console.WriteLine("7. Recent Messages");

            while (true)
            {
                System.Console.Write("Command: ");
                var request = System.Console.ReadLine();
                if (request == "quit")
                    break;

                int command;
                int.TryParse(request, out command);
                switch (command)
                {
                    case 1:
                        
                        Task.Run(async () =>
                        {
                            System.Console.Write("UserName: ");
                            var userName = System.Console.ReadLine();
                            
                            System.Console.Write("EmailAddress: ");
                            var emailAddress = System.Console.ReadLine();

                            System.Console.Write("Password: ");
                            var password = System.Console.ReadLine();

                            var logOnInfo = await client.Register(userName, emailAddress, password);

                            System.Console.WriteLine("User Registered {0}", logOnInfo.User.Name);
                        }).Wait();

                        break;
                    
                    case 2:

                        Task.Run(async () =>
                        {
                            System.Console.Write("UserName: ");
                            var userName = System.Console.ReadLine();

                            System.Console.Write("Password: ");
                            var password = System.Console.ReadLine();
                            
                            var logOnInfo = await client.Connect(userName, password);

                            System.Console.WriteLine("User Connected {0}", logOnInfo.User.Name);
                        }).Wait();

                        break;

                    case 3:

                        Task.Run(async () =>
                        {
                            System.Console.Write("RoomName: ");
                            var roomName = System.Console.ReadLine();

                            var roomId = await client.CreateRoom(roomName);
                            availibleRooms.Add(roomName, roomId);

                            System.Console.WriteLine("Room Created {0}", roomName);
                        }).Wait();

                        break;

                    case 4:

                        Task.Run(async () =>
                        {
                            System.Console.Write("RoomName: ");
                            var roomName = System.Console.ReadLine();

                            var roomId = availibleRooms[roomName];
                            await client.JoinRoom(roomId);

                            System.Console.WriteLine("Updated Rooms({0})", availibleRooms.Count);
                        }).Wait();

                        break;
                    
                    case 5:

                        Task.Run(async () =>
                        {
                            var rooms = await client.GetRooms();
                            availibleRooms.Clear();
                            foreach (var room in rooms)
                            {
                                availibleRooms.Add(room.Name, room.Id);
                            }

                            System.Console.WriteLine("Updated Rooms({0})", availibleRooms.Count);
                        }).Wait();

                        break;

                    case 6:

                        
                        Task.Run(async () =>
                        {
                            System.Console.Write("RoomName: ");
                            var roomName = System.Console.ReadLine();

                            System.Console.Write("Message: ");
                            var message = System.Console.ReadLine();

                            var roomId = availibleRooms[roomName];
                            await client.Send(message, roomId);

                            System.Console.WriteLine("Message Sent {0}", roomName);
                        }).Wait();

                        break;

                    case 7:

                        Task.Run(async () =>
                        {
                            System.Console.Write("RoomName: ");
                            var roomName = System.Console.ReadLine();

                            var roomId = availibleRooms[roomName];
                            var room = await client.GetRoomInfo(roomId);

                            var messages = room.RecentMessages
                                .Select(message => "{0} [{1}] {2}".FormatWith(message.CreatedAt, message.User.Name, message.Content))
                                .ToArray();

                            System.Console.WriteLine("{0} Recent Messages", room.Name);
                            System.Console.WriteLine(string.Join("\r\n", messages));
                        }).Wait();

                        break;
                }
            }

            client.Disconnect().Wait();
        }
    }
}
