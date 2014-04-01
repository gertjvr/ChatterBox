using ChatterBox.Core.Infrastructure.Facts;

namespace ChatterBox.Domain.Aggregates.UserAggregate.Facts
{
    public class UserCreatedFact : FactAbout<User>
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Hash { get; set; }
        
        public string Salt { get; set; }

        public string HashedPassword { get; set; }
    }
}