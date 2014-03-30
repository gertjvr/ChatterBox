using System;
using System.Linq;
using Autofac;
using ChatterBox.Core.Infrastructure;
using ChatterBox.Core.Persistence;
using Domain.Aggregates.ContactAggregate;
using Domain.Aggregates.ConversationAggregate;

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
                var repo = scope.Resolve<IRepository<Conversation>>();
                var convo = repo.GetById(Guid.Parse("1f3ae5d8-d02c-47c9-bb67-761ac0d13e03"));
            }

            var fredId = CreateContact("fred");
            var wilmaId = CreateContact("wilma");

            var conversationId = StartConversation("Topic", fredId, wilmaId);

            ChangeConverstationTopic(conversationId, "New Topic");

            SendMessageToConversation(conversationId, fredId, "Hello Wilma");

            var pebblesId = CreateContact("pebbles");

            AddContactToConversation(conversationId, pebblesId);
        }

        private static void AddContactToConversation(Guid conversationId, Guid pebblesId)
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var uow = scope.Resolve<IUnitOfWork>();
                var repo = scope.Resolve<IRepository<Conversation>>();
                var conversation = repo.GetById(conversationId);

                conversation.AddContact(pebblesId);

                uow.Complete();
            }
        }

        private static void ChangeConverstationTopic(Guid conversationId, string newTopic)
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var uow = scope.Resolve<IUnitOfWork>();
                var repo = scope.Resolve<IRepository<Conversation>>();
                var conversation = repo.GetById(conversationId);

                conversation.ChangeTopic(newTopic);

                uow.Complete();
            }
        }

        private static Guid CreateContact(string username)
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var uow = scope.Resolve<IUnitOfWork>();
                var repo = scope.Resolve<IRepository<Contact>>();
                var contact = Contact.Create(username);
                repo.Add(contact);
                uow.Complete();

                return contact.Id;
            }
        }

        private static Guid StartConversation(string topic, Guid ownerId, params Guid[] contacts)
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var uow = scope.Resolve<IUnitOfWork>();
                var repo = scope.Resolve<IRepository<Conversation>>();

                var conversation = Conversation.Create(topic, ownerId, contacts);

                repo.Add(conversation);
                uow.Complete();

                return conversation.Id;
            }
        }

        private static void SendMessageToConversation(Guid conversationId, Guid contactId, string content)
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var uow = scope.Resolve<IUnitOfWork>();
                var repo = scope.Resolve<IRepository<Conversation>>();
                var conversation = repo.GetById(conversationId);
                var clock = scope.Resolve<IClock>();

                conversation.AddMessage(contactId, content, clock.UtcNow);

                uow.Complete();
            }
        }
    }
}
