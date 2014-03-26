using System;
using ChatterBox.Core.Infrastructure.Entities;
using Domain.Aggregates.ClientAggregate.Facts;

namespace Domain.Aggregates.ClientAggregate
{
    public class Client : AggregateRoot
    {
        protected Client()
        {
        }

        public static Client Create(string name)
        {
            var fact = new ClientCreatedFact
            {
                AggregateRootId = Guid.NewGuid(),
                Name = name,
            };

            var customer = new Client();
            customer.Append(fact);
            customer.Apply(fact);
            return customer;
        }

        public void Apply(ClientCreatedFact fact)
        {
            Id = fact.AggregateRootId;
            Name = fact.Name;
        }

        public string Name { get; private set; }
    }
}