﻿using System.Linq;
using ChatterBox.ChatClient.Models;
using ChatterBox.Core.Mapping;
using ChatterBox.MessageContracts.Authentication.Request;
using ChatterBox.MessageContracts.Dtos;

namespace ChatterBox.ChatClient.Infrastructure.Mappers
{
    public class AuthenticateUserResponseToLogOnInfoMapper : IMapToNew<AuthenticateUserResponse, LogOnInfo>
    {
        private readonly IMapToNew<UserDto, User> _userMapper;
        private readonly IMapToNew<RoomDto, Room> _roomMapper;

        public AuthenticateUserResponseToLogOnInfoMapper(
            IMapToNew<UserDto, User> userMapper, 
            IMapToNew<RoomDto, Room> roomMapper)
        {
            _userMapper = userMapper;
            _roomMapper = roomMapper;
        }

        public LogOnInfo Map(AuthenticateUserResponse source)
        {
            if (source == null)
                return null;

            return new LogOnInfo
            {
                User = _userMapper.Map(source.User),
                Rooms = source.Rooms.Select(room => _roomMapper.Map(room)).ToList()
            };
        }
    }
}
