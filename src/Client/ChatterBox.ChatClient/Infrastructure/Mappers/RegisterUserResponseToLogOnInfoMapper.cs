using System.Linq;
using ChatterBox.ChatClient.Models;
using ChatterBox.Core.Mapping;
using ChatterBox.MessageContracts.Dtos;
using ChatterBox.MessageContracts.Users.Requests;

namespace ChatterBox.ChatClient.Infrastructure.Mappers
{
    public class RegisterUserResponseToLogOnInfoMapper : IMapToNew<RegisterUserResponse, LogOnInfo>
    {
        private readonly IMapToNew<UserDto, User> _userMapper;
        private readonly IMapToNew<RoomDto, Room> _roomMapper;

        public RegisterUserResponseToLogOnInfoMapper(
            IMapToNew<UserDto, User> userMapper, 
            IMapToNew<RoomDto, Room> roomMapper)
        {
            _userMapper = userMapper;
            _roomMapper = roomMapper;
        }

        public LogOnInfo Map(RegisterUserResponse source)
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