using ChatterBox.Core.Infrastructure.Facts;

namespace ChatterBox.Domain.Aggregates.UserAggregate.Facts
{
    public class UserStatusChangedFact : FactAbout<User>
    {
        public UserStatus Status { get; set; }
    }
}
