using ChatterBox.Core.Infrastructure.Facts;

namespace ChatterBox.Domain.Aggregates.UserAggregate.Facts
{
    public class UserPasswordChangedFact : FactAbout<User>
    {
        public string NewHashedPassword { get; set; }
    }
}