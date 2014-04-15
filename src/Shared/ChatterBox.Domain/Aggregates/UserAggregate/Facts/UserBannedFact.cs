using System;
using ChatterBox.Core.Infrastructure.Facts;

namespace ChatterBox.Domain.Aggregates.UserAggregate.Facts
{
    public class UserBannedFact : FactAbout<User>
    {
        public UserBannedFact(Guid aggregateRootId) : base(aggregateRootId)
        {   
        }
    }
}