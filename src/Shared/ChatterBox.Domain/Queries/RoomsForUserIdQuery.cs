﻿using System;
using System.Linq;
using ChatterBox.Core.Infrastructure.Queries;
using ChatterBox.Domain.Aggregates.RoomAggregate;

namespace ChatterBox.Domain.Queries
{
    public class RoomsForUserIdQuery : IQuery<Room>
    {
        private readonly Guid _userId;

        public RoomsForUserIdQuery(Guid userId)
        {
            _userId = userId;
        }

        public IQueryable<Room> Execute(IQueryable<Room> source)
        {
            return source.Where(room => room.Users.Contains(_userId));
        }
    }
}