using System;
using System.Linq;
using ChatterBox.Domain.Aggregates.RoomAggregate;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ThirdDrawer.Extensions.StringExtensionMethods;

namespace ChatterBox.Domain.Extensions
{
    public static class DomainExtensions
    {
        public static void EnsureAllowed(this User user, Room room)
        {
            if (room.PrivateRoom && !room.IsUserAllowed(user))
            {
                throw new Exception("You do not have access to {0}.".FormatWith(room.Name));
            }
        }

        public static bool IsUserAllowed(this Room room, User user)
        {
            return room.AllowedUsers.Contains(user.Id) || room.Owners.Contains(user.Id) || user.IsAdministrator();
        }

        public static void EnsureOpen(this Room room)
        {
            if (room.Closed)
            {
                throw new Exception("{0} is closed.".FormatWith(room.Name));
            }
        }

        public static void EnsureAdmin(this User user)
        {
            if (!user.IsAdministrator())
            {
                throw new Exception("You are not an admin.");
            }
        }

        public static void EnsureOwnerOrAdmin(this Room room, User user)
        {
            if (!room.Owners.Contains(user.Id) && user.IsAdministrator())
            {
                throw new Exception("You are not an owner of {0}.".FormatWith(room.Name));
            }
        }

        public static bool IsAdministrator(this User user)
        {
            return user.Role == UserRole.Admin;
        }
        
        public static bool IsBanned(this User user)
        {
            return user.Role == UserRole.Banned;
        }
    }
}