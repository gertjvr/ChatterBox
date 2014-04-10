using System;
using ChatterBox.Core.Infrastructure.Facts;

namespace ChatterBox.Domain.Aggregates.ClientAggregate.Facts
{
    [Serializable]
    public class ClientCreatedFact : FactAbout<Client>
    {
        public ClientCreatedFact(
            Guid aggregateRootId, 
            Guid userId, 
            string name)
            : base(aggregateRootId)
        {
            UserId = userId;
            Name = name;
        }

        public Guid UserId { get; protected set; }

        public string Name { get; protected set; }
    }
}