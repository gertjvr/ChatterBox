using System;
using System.Collections.Generic;
using System.Linq;
using ChatterBox.Core.Infrastructure;
using ChatterBox.Domain.Aggregates.MessageAggregate;
using ChatterBox.Domain.Aggregates.RoomAggregate;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.Domain.Properties;
using ThirdDrawer.Extensions.StringExtensionMethods;

namespace ChatterBox.Domain.Extensions
{
    public static class RepositoryExtensions
    {
        public static User VerifyUser(this IRepository<User> repository, Guid userId)
        {
            if (repository == null) 
                throw new ArgumentNullException("repository");

            var user = repository.GetById(userId);

            if (user == null)
            {
                throw new Exception("Unable to find {0}.".FormatWith(userId));
            }

            return user;
        }
        
        public static Room VerifyRoom(this IRepository<Room> repository, Guid roomId, bool mustBeOpen = false)
        {
            if (repository == null) 
                throw new ArgumentNullException("repository");

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

        public static IEnumerable<Room> GetAllowedRoomsByUserId(this IRepository<Room> repository, User user)
        {
            if (repository == null) 
                throw new ArgumentNullException("repository");

            if (user == null)
                throw new ArgumentNullException("user");

            return repository.Query(rooms => rooms.Where(room => room.Users.Contains(user.Id)).ToArray());
        }

        public static User GetByName(this IRepository<User> repository, string name)
        {
            if (repository == null)
                throw new ArgumentNullException("repository");

            if (name == null) 
                throw new ArgumentNullException("name");

            return repository.Query(users => users.SingleOrDefault(user => user.Name == name));
        }
        
        public static Room GetByName(this IRepository<Room> repository, string name)
        {
            if (repository == null) 
                throw new ArgumentNullException("repository");

            if (name == null) 
                throw new ArgumentNullException("name");

            return repository.Query(rooms => rooms.SingleOrDefault(room => room.Name == name));
        }

        public static IEnumerable<Message> GetMessagesForRoom(this IRepository<Message> repository, Guid roomId, int numberOfMessages = 10)
        {
            if (repository == null) 
                throw new ArgumentNullException("repository");

            return
                repository.Query(messages => messages.Where(message => message.RoomId == roomId) .OrderByDescending(message => message.CreatedAt)
                    .Take(numberOfMessages));
        }

        public static IEnumerable<Message> GetMessagesFromId(this IRepository<Message> repository, Guid fromId, int numberOfMessages = 10)
        {
            if (repository == null) 
                throw new ArgumentNullException("repository");

            if (fromId == Guid.Empty)
                throw new ArgumentException(LanguageResources.GuidCannotBeEmpty, "fromId");

            if (numberOfMessages > 0)
                throw new ArgumentOutOfRangeException("numberOfMessages");

            return repository.Query(messages => messages.OrderByDescending(message => message.CreatedAt)
                    .SkipWhile(message => message.Id != fromId)
                    .Take(numberOfMessages));
        } 
    }
}