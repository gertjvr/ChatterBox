using System.Linq;
using ChatterBox.ChatClient.Models;
using ChatterBox.Core.Mapping;
using ChatterBox.MessageContracts.Dtos;

namespace ChatterBox.ChatClient.Infrastructure.Mappers
{
    public class RoomDtoToRoomMapper : IMapToNew<RoomDto, Room>
    {
        private readonly IMapToNew<UserDto, User> _userMapper;
        private readonly IMapToNew<MessageDto, Message> _messageMapper;

        public RoomDtoToRoomMapper(
            IMapToNew<UserDto, User> userMapper,
            IMapToNew<MessageDto, Message> messageMapper)
        {
            _userMapper = userMapper;
            _messageMapper = messageMapper;
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
                Users = source.Users.Select(user => _userMapper.Map(user)).ToList(),
                Owners = source.Owners.Select(owner => _userMapper.Map(owner)).ToList(),
                RecentMessages = source.RecentMessages.Select(message => _messageMapper.Map(message)).ToList()
            };
        }
    }
}