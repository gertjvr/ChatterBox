using System;
using ChatterBox.Core.Infrastructure.Facts;

namespace ChatterBox.Domain.Aggregates.UserAggregate.Facts
{
    public class UserRoleChangedFact : FactAbout<User>
    {
        public UserRoleChangedFact(
            Guid aggregateRootId, 
            UserRole userRole) 
            : base(aggregateRootId)
        {
            UserRole = userRole;
        }

        public UserRole UserRole { get; protected set; }
    }
}