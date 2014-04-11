using System.Linq;
using ChatterBox.Core.Mapping;
using ChatterBox.Core.Persistence;
using ChatterBox.Domain.Aggregates.MessageAggregate;
using ChatterBox.Domain.Aggregates.RoomAggregate;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.Domain.Queries;
using ChatterBox.MessageContracts.Dtos;

namespace ChatterBox.ChatServer.Infrastructure.Mappers
{
    public class RoomToRoomDtoMapper : IMapToNew<Room, RoomDto>
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Message> _messageRepository;
        private readonly IMapToNew<User, UserDto> _userMapper;
        private readonly IMapToNew<Message, MessageDto> _messageMapper;

        public RoomToRoomDtoMapper(
            IRepository<User> userRepository,
            IMapToNew<User, UserDto> userMapper,
            IRepository<Message> messageRepository,
            IMapToNew<Message, MessageDto> messageMapper)
        {
            _userRepository = userRepository;
            _messageRepository = messageRepository;
            _userMapper = userMapper;
            _messageMapper = messageMapper;
        }

        public RoomDto Map(Room source)
        {
            if (source == null)
                return null;

            return new RoomDto
            {
                Name = source.Name,
                Count = 0,
                Private = source.Private,
                Topic = source.Topic,
                Closed = source.Closed,
                Welcome = source.Welcome,
                Contacts = source.Contacts.Select(contactId => _userMapper.Map(_userRepository.GetById(contactId))).ToArray(),
                Owners = source.Owners.Select(ownerId => _userMapper.Map(_userRepository.GetById(ownerId))).ToArray(),
                RecentMessages = _messageRepository.Query(new RecentMessagesQuery(15)).Select(message => _messageMapper.Map(message)),
            };
        }
    }
}