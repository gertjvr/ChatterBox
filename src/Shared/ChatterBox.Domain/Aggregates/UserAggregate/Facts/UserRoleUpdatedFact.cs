using System;
using ChatterBox.Core.Infrastructure.Facts;

namespace ChatterBox.Domain.Aggregates.UserAggregate.Facts
{
    public class UserRoleUpdatedFact : FactAbout<User>
    {
        public UserRoleUpdatedFact(
            Guid aggregateRootId, 
            UserRole userRole) 
            : base(aggregateRootId)
        {
            UserRole = userRole;
        }

        public UserRole UserRole { get; private set; }
    }
}