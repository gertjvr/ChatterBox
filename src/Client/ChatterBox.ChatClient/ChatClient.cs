﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using ChatterBox.ChatClient.ConfigurationSettings;
using ChatterBox.ChatClient.Models;
using ChatterBox.Core.Mapping;
using ChatterBox.MessageContracts.Authentication.Request;
using ChatterBox.MessageContracts.Dtos;
using ChatterBox.MessageContracts.Messages.Commands;
using ChatterBox.MessageContracts.Owners.Commands;
using ChatterBox.MessageContracts.Rooms.Commands;
using ChatterBox.MessageContracts.Rooms.Requests;
using ChatterBox.MessageContracts.Users.Commands;
using ChatterBox.MessageContracts.Users.Requests;
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
        private readonly IBus _bus;

        private IContainer _container;

        public ChatClient(string userAgent)
        {
            UserAgent = userAgent;

            InitializeLogger();

            _container = IoC.LetThereBeIoC();

            _bus = _container.Resolve<IBus>();

            StartNimbus((Bus) _bus);
        }

        public string UserAgent { get; private set; }

        public async Task<LogOnInfo> Register(string name, string emailAddress, string password)
        {
            var logOnInfoMapper = _container.Resolve<IMapToNew<RegisterUserResponse, LogOnInfo>>();
            var userMapper = _container.Resolve<IMapToNew<UserDto, User>>();

            var response = await _bus.Request(new RegisterUserRequest(name, emailAddress, password));

            _userContext.SetUserId(
                response.UserId, 
                userMapper.Map(response.User));

            await _bus.Send(new ConnectClientCommand(_clientContext.ClientId, UserAgent, _userContext.UserId));
            
            return logOnInfoMapper.Map(response);
        }
        
        public async Task<LogOnInfo> Connect(string name, string password)
        {
            var logOnInfoMapper = _container.Resolve<IMapToNew<AuthenticateUserResponse, LogOnInfo>>();
            var userMapper = _container.Resolve<IMapToNew<UserDto, User>>();

            var response = await _bus.Request(new AuthenticateUserRequest(name, password));

            _userContext.SetUserId(
                response.UserId, 
                userMapper.Map(response.User));

            await _bus.Send(new ConnectClientCommand(_clientContext.ClientId, UserAgent, _userContext.UserId));
            
            return logOnInfoMapper.Map(response);
        }

        public async Task<User> GetUserInfo()
        {
            var userMapper = _container.Resolve<IMapToNew<UserDto, User>>();

            var response = await _bus.Request(new UserInfoRequest(_userContext.UserId, _userContext.UserId));

            return userMapper.Map(response.User);
        }

        public async Task LogOut()
        {
            await _bus.Send(new DisconnectClientCommand(_clientContext.ClientId, _userContext.UserId));
        }

        public async Task Send(string message, Guid roomId)
        {
            await _bus.Send(new SendMessageCommand(message, roomId, _userContext.UserId));
        }

        public async Task<Guid> CreateRoom(string roomName)
        {
            var roomMapper = _container.Resolve<IMapToNew<RoomDto, Room>>();

            var response = await _bus.Request(new CreateRoomRequest(roomName, _userContext.UserId));

            return response.RoomId;
        }

        public async Task JoinRoom(Guid roomId)
        {
            await _bus.Send(new JoinRoomCommand(roomId, _userContext.UserId, _userContext.UserId));
        }

        public async Task LeaveRoom(Guid roomId)
        {
            await _bus.Send(new LeaveRoomCommand(roomId, _userContext.UserId, _userContext.UserId));
        }

        public async Task SendPrivateMessage(string message, Guid userId)
        {
            await _bus.Send(new SendPrivateMessageCommand(message, userId, _userContext.UserId));
        }

        public async Task Kick(Guid userId, Guid roomId)
        {
            await _bus.Send(new KickUserCommand(userId, roomId, _userContext.UserId));
        }

        public async Task<IEnumerable<Message>> GetPreviousMessages(Guid fromId)
        {
            var messageMapper = _container.Resolve<IMapToNew<MessageDto, Message>>();

            var response = await _bus.Request(new PreviousMessagesRequest(_userContext.UserId, fromId, 10, _userContext.UserId));

            return response.Messages.Select(messageMapper.Map).ToArray();
        }

        public async Task<Room> GetRoomInfo(Guid roomId)
        {
            var roomMapper = _container.Resolve<IMapToNew<RoomDto, Room>>();

            var response = await _bus.Request(new RoomInfoRequest(roomId, _userContext.UserId));

            return roomMapper.Map(response.Room);
        }

        public async Task<IEnumerable<Room>> GetRooms()
        {
            var roomMapper = _container.Resolve<IMapToNew<RoomDto, Room>>();

            var response = await _bus.Request(new AllowedRoomsRequest(_userContext.UserId, _userContext.UserId));

            return response.Rooms.Select(roomMapper.Map);
        }

        public async Task Disconnect()
        {
            await _bus.Send(new DisconnectClientCommand(_clientContext.ClientId, _userContext.UserId));

            IContainer container = _container;
            if (container != null) container.Dispose();
            _container = null;
        }

        private void StartNimbus(Bus bus)
        {
            bus.Start();
        }

        private static void InitializeLogger()
        {
            var minimumLogLevel = (LogEventLevel)Enum.Parse(typeof(LogEventLevel), DefaultSettingsReader.Get<MinimumLogLevelSetting>());
            var serverUrl = DefaultSettingsReader.Get<SeqServerUriSetting>();
            var logPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), @"ClientConsoleLog-{Date}.txt");

            LoggerConfiguration logConfiguration = new LoggerConfiguration()
                .MinimumLevel.Is(minimumLogLevel)
                .WriteTo.Trace()
                .WriteTo.RollingFile(logPath)
                .WriteTo.Seq(serverUrl);

            if (Environment.UserInteractive && minimumLogLevel == LogEventLevel.Verbose)
            {
                logConfiguration.WriteTo.ColoredConsole();
            }

            Log.Logger = logConfiguration.CreateLogger();
        }
    }
}