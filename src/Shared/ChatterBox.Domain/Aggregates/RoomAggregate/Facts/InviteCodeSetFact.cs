﻿using System;
using ChatterBox.Core.Infrastructure.Facts;

namespace ChatterBox.Domain.Aggregates.RoomAggregate.Facts
{
    public class InviteCodeSetFact : FactAbout<Room>
    {
        public InviteCodeSetFact(Guid aggregateRootId, string inviteCode)
            : base(aggregateRootId)
        {
            if (inviteCode == null) 
                throw new ArgumentNullException("inviteCode");

            InviteCode = inviteCode;
        }

        public string InviteCode { get; private set; }
    }
}