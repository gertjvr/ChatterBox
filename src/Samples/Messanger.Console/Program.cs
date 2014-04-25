using System;
using Autofac;
using ChatterBox.Core.Extensions;
using ChatterBox.Core.Infrastructure;
using ChatterBox.Core.Services;
using ChatterBox.Domain.Aggregates.MessageAggregate;
using ChatterBox.Domain.Aggregates.RoomAggregate;
using ChatterBox.Domain.Aggregates.UserAggregate;

namespace Messanger.Console
{
    public class Program
    {
        private static IContainer _container;

        public static void Main(string[] args)
        {
            _container = IoC.LetThereBeIoC();

            var fred = CreateUser("fred", "fred@rocks.com", "test@password");
            var wilma = CreateUser("wilma", "wilma@rocks.com", "test@password");

            var roomId = CreateRoom(string.Empty, fred);

            ChangeRoomTopic(roomId, "New Topic");

            CreateMessage(roomId, fred, "Hello Wilma");

            var pebbles = CreateUser("pebbles", "pebbles@rocks.com", "test@password");

            AddUserToRoom(roomId, pebbles);
        }

        private static void AddUserToRoom(Guid roomId, User user)
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var uow = scope.Resolve<IUnitOfWork>();
                var roomRepo = scope.Resolve<IRepository<Room>>();

                var room = roomRepo.GetById(roomId);

                room.Join(user);

                uow.Complete();
            }
        }

        private static void ChangeRoomTopic(Guid roomId, string newTopic)
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var uow = scope.Resolve<IUnitOfWork>();
                var repo = scope.Resolve<IRepository<Room>>();
                
                var room = repo.GetById(roomId);

                room.UpdateTopic(newTopic);

                uow.Complete();
            }
        }

        private static User CreateUser(string name,string email, string password)
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var uow = scope.Resolve<IUnitOfWork>();
                var repo = scope.Resolve<IRepository<User>>();
                var crypto = scope.Resolve<ICryptoService>();
                var clock = scope.Resolve<IClock>();

                var salt = crypto.CreateSalt();
                var user = new User(name, email, email.ToMD5(), salt, password.ToSha256(salt), clock.UtcNow);
                repo.Add(user);
                uow.Complete();

                return user;
            }
        }

        private static Guid CreateRoom(string name, User owner)
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var uow = scope.Resolve<IUnitOfWork>();
                var repo = scope.Resolve<IRepository<Room>>();

                var room = new Room(name, owner.Id, string.Empty, string.Empty, false);

                repo.Add(room);
                uow.Complete();

                return room.Id;
            }
        }

        private static Guid CreateMessage(Guid roomId, User user, string content)
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var uow = scope.Resolve<IUnitOfWork>();
                var repo = scope.Resolve<IRepository<Message>>();
                var clock = scope.Resolve<IClock>();

                var message = new Message(roomId, user.Id, content, clock.UtcNow);

                repo.Add(message);
                uow.Complete();

                return message.Id;
            }
        }
    }
}
