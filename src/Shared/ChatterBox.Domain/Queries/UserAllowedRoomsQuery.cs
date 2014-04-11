using System;
using System.Linq;
using ChatterBox.Core.Infrastructure.Queries;
using ChatterBox.Domain.Aggregates.RoomAggregate;

namespace ChatterBox.Domain.Queries
{
    public class UserAllowedRoomsQuery : IQuery<Room>
    {
        private readonly Guid _userId;

        public UserAllowedRoomsQuery(Guid userId)
        {
            _userId = userId;
        }

        public IQueryable<Room> Execute(IQueryable<Room> source)
        {
            return source.Where(room => room.Contacts.Contains(_userId));
        }
    }
}