using System;
using System.Linq;
using ChatterBox.Core.Infrastructure;
using ChatterBox.Core.Mapping;
using ChatterBox.Domain.Aggregates.MessageAggregate;
using ChatterBox.Domain.Aggregates.RoomAggregate;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.Domain.Extensions;
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
            IRepository<Message> messageRepository,
            IMapToNew<User, UserDto> userMapper,
            IMapToNew<Message, MessageDto> messageMapper)
        {
            if (userRepository == null) 
                throw new ArgumentNullException("userRepository");
            
            if (messageRepository == null) 
                throw new ArgumentNullException("messageRepository");
            
            if (userMapper == null) 
                throw new ArgumentNullException("userMapper");
            
            if (messageMapper == null) 
                throw new ArgumentNullException("messageMapper");

            _userRepository = userRepository;
            _messageRepository = messageRepository;
            _userMapper = userMapper;
            _messageMapper = messageMapper;
        }

        public RoomDto Map(Room source)
        {
            if (source == null)
                return null;

            var users = source.Users.Select(userId => _userMapper.Map(_userRepository.Query(users1 => users1.First(user => user.Id == userId)))).ToArray();
            var owners = source.Owners.Select(ownerId => _userMapper.Map(_userRepository.Query(users1 => users1.First(user => user.Id == ownerId)))).ToArray();
            var recentMessages = _messageRepository.GetMessages().Select(message => _messageMapper.Map(message));

            return new RoomDto(
                source.Name,
                0,
                source.PrivateRoom,
                source.Topic,
                source.Closed,
                source.WelcomeMessage,
                users,
                owners,
                recentMessages);
        }
    }
}