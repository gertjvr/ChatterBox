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

            //using (var scope = _container.BeginLifetimeScope())
            //{
            //    var uow = scope.Resolve<IUnitOfWork>();
            //    var repo = scope.Resolve<IRepository<Room>>();
            //    var convo = repo.GetById(Guid.Parse("1f3ae5d8-d02c-47c9-bb67-761ac0d13e03"));
            //}

            var fredId = CreateUser("fred", "fred@rocks.com", "test@password");
            var wilmaId = CreateUser("wilma", "wilma@rocks.com", "test@password");

            var roomId = CreateRoom(string.Empty, fredId);

            ChangeRoomTopic(roomId, "New Topic");

            CreateMessage(roomId, fredId, "Hello Wilma");

            var pebblesId = CreateUser("pebbles", "pebbles@rocks.com", "test@password");

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
                var repo = uow.Repository<Room>();
                
                var conversation = repo.GetById(userId);
                
                conversation.ChangeTopic(newTopic);

                uow.Complete();
            }
        }

        private static Guid CreateUser(string name,string email, string password)
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var uow = scope.Resolve<IUnitOfWork>();
                var repo = uow.Repository<User>();
                var crypto = scope.Resolve<ICryptoService>();

                var salt = crypto.CreateSalt();
                var user = new User(name, email, email.ToMD5(), salt, password.ToSha256(salt));
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
                var repo = uow.Repository<Room>();

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
                
                var message = Message.Create(roomId, userId, content, DateTimeHelper.UtcNow);

                repo.Add(message);
                uow.Complete();

                return message.Id;
            }
        }
    }
}
