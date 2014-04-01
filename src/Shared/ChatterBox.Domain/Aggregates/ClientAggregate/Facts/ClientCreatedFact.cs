using System;
using ChatterBox.Core.Infrastructure.Facts;

namespace Domain.Aggregates.ClientAggregate.Facts
{
    [Serializable]
    public class ClientCreatedFact : FactAbout<Client>
    {
        public Guid UserId { get; set; }
        
        public string Name { get; set; }
    }
}