﻿using System;
using ChatterBox.Core.Infrastructure.Facts;
using ChatterBox.Domain.Properties;

namespace ChatterBox.Domain.Aggregates.RoomAggregate.Facts
{
    public class UserLeftFact : FactAbout<Room>
    {
        public UserLeftFact(
            Guid aggregateRootId, 
            Guid userId)
            : base(aggregateRootId)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException(LanguageResources.GuidCannotBeEmpty, "userId");

            UserId = userId;
        }

        public Guid UserId { get; private set; }
    }
}