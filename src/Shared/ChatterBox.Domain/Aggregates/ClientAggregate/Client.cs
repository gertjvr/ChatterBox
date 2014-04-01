using System;
using ChatterBox.Core.Infrastructure.Entities;
using Domain.Aggregates.ClientAggregate.Facts;

namespace Domain.Aggregates.ClientAggregate
{
    [Serializable]
    public class Client : AggregateRoot
    {
        protected Client()
        {
        }

        public static Client Create(Guid userId, string name)
        {
            var fact = new ClientCreatedFact
            {
                AggregateRootId = Guid.NewGuid(),
                UserId = userId,
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
            UserId = fact.UserId;
            Name = fact.Name;
        }

        public Guid UserId { get; protected set; }
        
        public string Name { get; protected set; }
    }
}