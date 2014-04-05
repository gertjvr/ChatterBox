using System;
using ChatterBox.Core.Infrastructure.Facts;

namespace ChatterBox.Domain.Aggregates.UserAggregate.Facts
{
    public class UserLastActivityUpdatedFact : FactAbout<User>
    {
        public DateTimeOffset LastActivity { get; set; }
    }
}