﻿using System;
using ChatterBox.Core.Infrastructure.Facts;

namespace ChatterBox.Domain.Aggregates.RoomAggregate.Facts
{
    public class UserAllowedFact : FactAbout<Room>
    {
        public UserAllowedFact(
            Guid aggregateRootId, 
            Guid userId)
            : base(aggregateRootId)
        {
            UserId = userId;
        }

        public Guid UserId { get; protected set; }
    }
}