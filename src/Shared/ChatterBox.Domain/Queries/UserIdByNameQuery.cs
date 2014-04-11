using System;
using System.Linq;
using ChatterBox.Core.Infrastructure.Queries;
using ChatterBox.Domain.Aggregates.UserAggregate;

namespace ChatterBox.Domain.Queries
{
    public class UserIdByNameQuery : IQuery<User, Guid>
    {
        private readonly string _name;

        public UserIdByNameQuery(string name)
        {
            _name = name;
        }

        public Guid Execute(IQueryable<User> source)
        {
            var target = source.SingleOrDefault(user => user.Name == _name);

            return (target != null) ? target.Id : Guid.Empty;
        }
    }
}