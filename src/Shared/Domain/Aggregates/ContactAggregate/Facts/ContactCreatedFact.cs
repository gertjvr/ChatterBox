using ChatterBox.Core.Infrastructure.Facts;

namespace Domain.Aggregates.ContactAggregate.Facts
{
    public class ContactCreatedFact : FactAbout<Contact>
    {
        public string Username { get; set; }
    }
}