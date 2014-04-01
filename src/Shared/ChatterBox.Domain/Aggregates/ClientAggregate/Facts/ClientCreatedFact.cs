using System;
using ChatterBox.Core.Infrastructure.Facts;

namespace ChatterBox.Domain.Aggregates.ClientAggregate.Facts
{
    [Serializable]
    public class ClientCreatedFact : FactAbout<Client>
    {
        public Guid UserId { get; set; }
        
        public string Name { get; set; }
    }
}