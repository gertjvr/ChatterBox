using ChatterBox.Core.Infrastructure.Facts;

namespace ChatterBox.Domain.Aggregates.UserAggregate.Facts
{
    public class UserNameChangedFact : FactAbout<User>
    {
        public string NewUserName { get; set; }
    }
}