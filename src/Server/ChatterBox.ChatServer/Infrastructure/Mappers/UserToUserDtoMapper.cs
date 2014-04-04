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
                Active = source.Active,
                Status = (int)source.Status,
                Note = source.Note,
                AfkNote = source.AfkNote,
                IsAfk = source.IsAfk,
                Flag = source.Flag,
                Country = source.Country,
                LastActivity = source.LastActivity,
                IsAdmin = source.IsAdmin,
            };
        }
    }
}

