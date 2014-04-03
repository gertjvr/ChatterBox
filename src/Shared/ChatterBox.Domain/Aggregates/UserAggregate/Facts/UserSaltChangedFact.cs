using ChatterBox.Core.Infrastructure.Facts;

namespace ChatterBox.Domain.Aggregates.UserAggregate.Facts
{
    public class UserSaltChangedFact : FactAbout<User>
    {
        public string NewSalt { get; set; }
    }
}
