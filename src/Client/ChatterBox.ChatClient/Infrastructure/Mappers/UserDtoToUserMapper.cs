using ChatterBox.ChatClient.Models;
using ChatterBox.Core.Mapping;
using ChatterBox.MessageContracts.Dtos;

namespace ChatterBox.ChatClient.Infrastructure.Mappers
{
    public class UserDtoToUserMapper : IMapToNew<UserDto, User>
    {
        public User Map(UserDto source)
        {
            if (source == null)
                return null;

            return new User
            {
                Name = source.Name,
                Hash = source.Hash,
                Status = (UserStatus)source.Status,
                Note = source.Note,
                LastActivity = source.LastActivity,
                UserRole = (UserRole)source.UserRole
            };
        }
    }
}