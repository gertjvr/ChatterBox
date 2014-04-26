using ChatterBox.ChatClient.Models;
using ChatterBox.Core.Mapping;
using ChatterBox.MessageContracts.Dtos;

namespace ChatterBox.ChatClient.Infrastructure.Mappers
{
    public class MessageDtoToMessageMapper : IMapToNew<MessageDto, Message>
    {
        private readonly IMapToNew<UserDto, User> _userMapper;

        public MessageDtoToMessageMapper(IMapToNew<UserDto, User> userMapper)
        {
            _userMapper = userMapper;
        }

        public Message Map(MessageDto source)
        {
            if (source == null)
                return null;

            return new Message
            {
                Id = source.Id,
                Content = source.Content,
                User = _userMapper.Map(source.User),
                CreatedAt = source.CreatedAt
            };
        }
    }
}