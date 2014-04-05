using ChatterBox.Core.Mapping;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.MessageContracts.Dtos;

namespace ChatterBox.ChatServer.Infrastructure.Mappers
{
    public class UserToUserDtoMapper : IMapToNew<User, UserDto>
    {
        public UserDto Map(User source)
        {
            if (source == null)
                return null;

            return new UserDto
            {
                Name = source.Name,
                Hash = source.Hash,
                Status = (int)source.Status,
                LastActivity = source.LastActivity,
                UserRole = (int)source.UserRole,
            };
        }
    }
}

