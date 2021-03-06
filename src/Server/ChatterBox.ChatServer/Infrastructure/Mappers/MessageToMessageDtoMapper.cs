using System;
using ChatterBox.Core.Infrastructure;
using ChatterBox.Core.Mapping;
using ChatterBox.Domain.Aggregates.MessageAggregate;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.MessageContracts.Dtos;

namespace ChatterBox.ChatServer.Infrastructure.Mappers
{
    public class MessageToMessageDtoMapper : IMapToNew<Message, MessageDto>
    {
        private readonly IRepository<User> _userRepository;
        private readonly IMapToNew<User, UserDto> _userMapper;

        public MessageToMessageDtoMapper(
            IRepository<User> userRepository,
            IMapToNew<User, UserDto> userMapper)
        {
            if (userRepository == null) 
                throw new ArgumentNullException("userRepository");

            if (userMapper == null) 
                throw new ArgumentNullException("userMapper");

            _userRepository = userRepository;
            _userMapper = userMapper;
        }

        public MessageDto Map(Message source)
        {
            if (source == null)
                return null;

            var user = _userRepository.GetById(source.UserId);

            return new MessageDto(
                source.Id,
                source.Content,
                source.CreatedAt,
                _userMapper.Map(user));
        }
    }
}