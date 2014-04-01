using System;
using System.Linq;
using Autofac;
using ChatterBox.Core.Infrastructure;
using ChatterBox.Core.Persistence;
using Domain.Aggregates.MessageAggregate;
using Domain.Aggregates.RoomAggregate;
using Domain.Aggregates.UserAggregate;

namespace Messanger.Console
{
    public class Program
    {
        private static IContainer _container;

        public static void Main(string[] args)
        {
            _container = IoC.LetThereBeIoC();

            using (var scope = _container.BeginLifetimeScope())
            {
                var uow = scope.Resolve<IUnitOfWork>();
                var repo = scope.Resolve<IRepository<Room>>();
                var convo = repo.GetById(Guid.Parse("1f3ae5d8-d02c-47c9-bb67-761ac0d13e03"));
            }

            var fredId = CreateUser("fred");
            var wilmaId = CreateUser("wilma");

            var roomId = CreateRoom(string.Empty, fredId, wilmaId);

            ChangeRoomTopic(roomId, "New Topic");

            CreateMessage(roomId, fredId, "Hello Wilma");

            var pebblesId = CreateUser("pebbles");

            AddUserToRoom(roomId, pebblesId);
        }

        private static void AddUserToRoom(Guid roomId, Guid userId)
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var uow = scope.Resolve<IUnitOfWork>();
                var repo = scope.Resolve<IRepository<Room>>();

                var conversation = repo.GetById(roomId);

                conversation.AddUser(userId);

                uow.Complete();
            }
        }

        private static void ChangeRoomTopic(Guid userId, string newTopic)
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var uow = scope.Resolve<IUnitOfWork>();
                var repo = scope.Resolve<IRepository<Room>>();
                
                var conversation = repo.GetById(userId);
                
                conversation.ChangeTopic(newTopic);

                uow.Complete();
            }
        }

        private static Guid CreateUser(string name,string email,string hash, string salt, string hashedPassword)
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var uow = scope.Resolve<IUnitOfWork>();
                var repo = scope.Resolve<IRepository<User>>();
                
                var user = User.Create(name, email, hash, salt, hashedPassword);
                repo.Add(user);
                uow.Complete();

                return user.Id;
            }
        }

        private static Guid CreateRoom(string topic, Guid ownerId, params Guid[] users)
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var uow = scope.Resolve<IUnitOfWork>();
                var repo = scope.Resolve<IRepository<Room>>();

                var room = Room.Create(topic, ownerId, users);

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

                var message = Message.Create(roomId, userId, content, clock.UtcNow);

                repo.Add(message);
                uow.Complete();

                return message.Id;
            }
        }
    }
}
