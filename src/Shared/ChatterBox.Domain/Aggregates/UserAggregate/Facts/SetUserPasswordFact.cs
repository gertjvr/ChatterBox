using ChatterBox.Core.Infrastructure.Facts;

namespace Domain.Aggregates.UserAggregate.Facts
{
    public class SetUserPasswordFact : FactAbout<User>
    {
        public string HashedPassword { get; set; }
    }
}