using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using ChatterBox.ChatClient.ConfigurationSettings;
using ChatterBox.ChatClient.Models;
using ChatterBox.Core.Mapping;
using ChatterBox.MessageContracts.Commands;
using ChatterBox.MessageContracts.Dtos;
using ChatterBox.MessageContracts.Requests;
using ConfigInjector.QuickAndDirty;
using Nimbus;
using Seq;
using Serilog;
using Serilog.Events;

namespace ChatterBox.ChatClient
{
    public class ChatClient : IChatClient
    {
        private readonly UserContext _userContext = new UserContext();
        private readonly ClientContext _clientContext = new ClientContext();
        private IContainer _container;
        private IBus _bus;

        public ChatClient()
        {
            InitializeLogger();

            _container = IoC.LetThereBeIoC();

            StartNimbus((Bus) _container.Resolve<IBus>());
        }

        public async Task<LogOnInfo> Connect(string name, string password)
        {
            var logOnInfoMapper = _container.Resolve<IMapToNew<AuthenticateUserResponse, LogOnInfo>>();
            var userMapper = _container.Resolve<IMapToNew<UserDto, User>>();

            var response = await _bus.Request(new AuthenticateUserRequest(name, password));

            _clientContext.SetClientId(response.ClientId);

            _userContext.SetUserId(
                response.UserId, 
                userMapper.Map(response.User));
            
            return logOnInfoMapper.Map(response);
        }

        public async Task<User> GetUserInfo()
        {
            var userMapper = _container.Resolve<IMapToNew<UserDto, User>>();

            var response = await _bus.Request(new GetUserInfoRequest(_userContext.UserId));

            return userMapper.Map(response.User);
        }

        public async Task LogOut()
        {
            await _bus.Send(new DisconnectClientCommand(_clientContext.ClientId));
        }

        public async Task Send(string message, Guid roomId)
        {
            await _bus.Send(new SendMessageCommand(message, roomId, _userContext.UserId));
        }

        public async Task CreateRoom(string roomName)
        {
            await _bus.Request(new CreateRoomRequest(roomName, _userContext.UserId));
        }

        public async Task JoinRoom(Guid roomId)
        {
            await _bus.Send(new JoinRoomCommand(roomId, _userContext.UserId));
        }

        public async Task LeaveRoom(Guid roomId)
        {
            await _bus.Send(new LeaveRoomCommand(roomId, _userContext.UserId));
        }

        public async Task SendPrivateMessage(Guid userId, string message)
        {
            await _bus.Send(new SendPrivateMessageCommand(userId, message, _userContext.UserId));
        }

        public async Task Kick(Guid userId, Guid roomId)
        {
            await _bus.Send(new KickUserCommand(userId, roomId, _userContext.UserId));
        }

        public async Task<IEnumerable<Message>> GetPreviousMessages(Guid fromId)
        {
            var messageMapper = _container.Resolve<IMapToNew<MessageDto, Message>>();

            var response = await _bus.Request(new GetPreviousMessagesRequest(fromId));

            return response.Messages.Select(messageMapper.Map).ToArray();
        }

        public async Task<Room> GetRoomInfo(Guid roomId)
        {
            var roomMapper = _container.Resolve<IMapToNew<RoomDto, Room>>();

            var response = await _bus.Request(new GetRoomInfoRequest(_userContext.UserId));

            return roomMapper.Map(response.Room);
        }

        public async Task<IEnumerable<Room>> GetRooms()
        {
            var roomMapper = _container.Resolve<IMapToNew<RoomDto, Room>>();

            var response = await _bus.Request(new GetAllowedRoomsRequest(_userContext.UserId));

            return response.Rooms.Select(roomMapper.Map);
        }

        public void Disconnect()
        {
            _bus.Send(new DisconnectClientCommand(_clientContext.ClientId));

            IContainer container = _container;
            if (container != null) container.Dispose();
            _container = null;
        }

        private void StartNimbus(Bus bus)
        {
            bus.Start();

            _bus = bus;
        }

        private static void InitializeLogger()
        {
            var minimumLogLevel = DefaultSettingsReader.Get<MinimumLogLevelSetting>();

            LoggerConfiguration logConfiguration = new LoggerConfiguration()
                .WriteTo.Seq(DefaultSettingsReader.Get<SeqServerUriSetting>())
                .WriteTo.Trace()
                .WriteTo.Trace()
                .WriteTo.RollingFile(
                    Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                        @"ClientConsoleLog-{Date}.txt"))
                .MinimumLevel.Is((LogEventLevel)Enum.Parse(typeof(LogEventLevel), minimumLogLevel));

            if (Environment.UserInteractive)
            {
                logConfiguration.WriteTo.ColoredConsole();
            }

            Log.Logger = logConfiguration.CreateLogger();
        }
    }
}