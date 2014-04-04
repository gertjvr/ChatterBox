using ChatterBox.Core.Mapping;
using ChatterBox.Core.Persistence;
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
            _userRepository = userRepository;
            _userMapper = userMapper;
        }

        public MessageDto Map(Message source)
        {
            if (source == null)
                return null;

            return new MessageDto
            {
                Id = source.Id,
                Content = source.Content,
                User = _userMapper.Map(_userRepository.GetById(source.UserId)),
                CreatedAt = source.CreatedAt,
            };
        }
    }
}