using ChatterBox.Core.Infrastructure.Facts;

namespace ChatterBox.Domain.Aggregates.UserAggregate.Facts
{
    public class UserRoleChangedFact : FactAbout<User>
    {
        public UserRole UserRole { get; set; }
    }
}