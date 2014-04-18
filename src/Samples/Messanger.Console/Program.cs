using System;
using Autofac;
using ChatterBox.Core.Extensions;
using ChatterBox.Core.Infrastructure;
using ChatterBox.Core.Persistence;
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

            var fredId = CreateUser("fred", "fred@rocks.com", "test@password");
            var wilmaId = CreateUser("wilma", "wilma@rocks.com", "test@password");

            var roomId = CreateRoom(string.Empty, fredId);

            ChangeRoomTopic(roomId, "New Topic", fredId);

            CreateMessage(roomId, fredId, "Hello Wilma");

            var pebblesId = CreateUser("pebbles", "pebbles@rocks.com", "test@password");

            AddUserToRoom(roomId, pebblesId);
        }

        private static void AddUserToRoom(Guid roomId, Guid userId)
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var uow = scope.Resolve<IUnitOfWork>();
                var userRepo = scope.Resolve<IRepository<User>>();
                var roomRepo = scope.Resolve<IRepository<Room>>();

                var user = userRepo.GetById(roomId);
                var room = roomRepo.GetById(roomId);

                room.Join(user);

                uow.Complete();
            }
        }

        private static void ChangeRoomTopic(Guid roomId, string newTopic, Guid userId)
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var uow = scope.Resolve<IUnitOfWork>();
                var repo = scope.Resolve<IRepository<Room>>();
                
                var room = repo.GetById(roomId);

                room.ChangeTopic(newTopic);

                uow.Complete();
            }
        }

        private static Guid CreateUser(string name,string email, string password)
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

                return user.Id;
            }
        }

        private static Guid CreateRoom(string name, Guid ownerId)
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var uow = scope.Resolve<IUnitOfWork>();
                var repo = scope.Resolve<IRepository<Room>>();

                var room = new Room(name, ownerId);

                repo.Add(room);
                uow.Complete();

                return room.Id;
            }
        }

        private static Guid CreateMessage(Guid roomId, Guid userId, string content)
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var uow = scope.Resolve<IUnitOfWork>();
                var repo = scope.Resolve<IRepository<Message>>();
                var clock = scope.Resolve<IClock>();

                var message = new Message(roomId, userId, content, clock.UtcNow);

                repo.Add(message);
                uow.Complete();

                return message.Id;
            }
        }
    }
}
