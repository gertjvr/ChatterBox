using ChatterBox.Core.Infrastructure.Facts;

namespace Domain.Aggregates.ClientAggregate.Facts
{
    public class ClientCreatedFact : FactAbout<Client>
    {
        public string Name { get; set; }
    }
}