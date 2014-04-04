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
                Active = source.Active,
                Status = (UserStatus)source.Status,
                Note = source.Note,
                AfkNote = source.AfkNote,
                IsAfk = source.IsAfk,
                Flag = source.Flag,
                Country = source.Country,
                LastActivity = source.LastActivity,
                IsAdmin = source.IsAdmin
            };
        }
    }
}