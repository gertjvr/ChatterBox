using System;
using ChatterBox.Core.Persistence;
using ChatterBox.Domain.Aggregates.RoomAggregate;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ThirdDrawer.Extensions.StringExtensionMethods;

namespace ChatterBox.Domain.Extensions
{
    public static class RepositoryExtensions
    {
        public static User VerifyUser(this IRepository<User> repository, Guid userId)
        {
            var user = repository.GetById(userId);

            if (user == null)
            {
                throw new Exception("Unable to find {0}.".FormatWith(userId));
            }

            return user;
        }

        public static Room VerifyRoom(this IRepository<Room> repository, Guid roomId, bool mustBeOpen = false)
        {
            var room = repository.GetById(roomId);

            if (room == null)
            {
                throw new Exception("Unable to find {0}.".FormatWith(roomId));
            }

            if (room.Closed && mustBeOpen)
            {
                throw new Exception("{0} is closed.".FormatWith(room.Name));
            }

            return room;
        }
    }
}