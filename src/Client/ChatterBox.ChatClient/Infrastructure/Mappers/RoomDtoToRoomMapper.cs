using System.Linq;
using ChatterBox.ChatClient.Models;
using ChatterBox.Core.Mapping;
using ChatterBox.MessageContracts.Dtos;

namespace ChatterBox.ChatClient.Infrastructure.Mappers
{
    public class RoomDtoToRoomMapper : IMapToNew<RoomDto, Room>
    {
        private readonly IMapToNew<UserDto, User> _userMapper;

        public RoomDtoToRoomMapper(
            IMapToNew<UserDto, User> userMapper)
        {
            _userMapper = userMapper;
        }

        public Room Map(RoomDto source)
        {
            if (source == null)
                return null;

            return new Room
            {
                Name = source.Name,
                Count = source.Count,
                Private = source.PrivateRoom,
                Topic = source.Topic,
                Closed = source.Closed,
                Welcome = source.WelcomeMessage,
                Contacts = source.Users.Select(user => _userMapper.Map(user)).ToList()
            };
        }
    }
}