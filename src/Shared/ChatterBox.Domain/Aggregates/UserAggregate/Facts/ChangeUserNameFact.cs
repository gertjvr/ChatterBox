using ChatterBox.Core.Infrastructure.Facts;

namespace Domain.Aggregates.UserAggregate.Facts
{
    public class ChangeUserNameFact : FactAbout<User>
    {
        public string NewUserName { get; set; }
    }
}