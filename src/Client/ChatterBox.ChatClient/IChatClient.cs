using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChatterBox.ChatClient.Models;

namespace ChatterBox.ChatClient
{
    public interface IChatClient
    {
        Task<LogOnInfo> Connect(string name, string password);
        Task<User> GetUserInfo();
        Task LogOut();
        Task Send(string message, Guid roomId);
        Task<Guid> CreateRoom(string roomName);
        Task JoinRoom(Guid roomId);
        Task LeaveRoom(Guid roomId);
        Task SendPrivateMessage(string message, Guid userId);
        Task Kick(Guid userId, Guid roomId);
        Task<IEnumerable<Message>> GetPreviousMessages(Guid fromId);
        Task<Room> GetRoomInfo(Guid roomId);
        Task<IEnumerable<Room>> GetRooms();
        void Disconnect();
    }
}