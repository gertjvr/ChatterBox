using System;
using Autofac;
using ChatterBox.Core.Persistence;
using Domain.Aggregates.ContactAggregate;

namespace Messanger.Console
{
    public class Program
    {
        private static IContainer _container;

        public static void Main(string[] args)
        {
            _container = IoC.LetThereBeIoC();

            var fred = CreateContact("fred");
            var wilma = CreateContact("wilma");


        }

        private static Guid CreateContact(string username)
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var uow = scope.Resolve<IUnitOfWork>();
                var repo = scope.Resolve<IRepository<Contact>>();
                var contact = new Contact(username);
                repo.Add(contact);
                uow.Complete();

                return contact.Id;
            }
        }
    }
}
