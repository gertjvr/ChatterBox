using System.Linq;
using ChatterBox.Core.Infrastructure.Queries;
using ChatterBox.Domain.Aggregates.UserAggregate;

namespace ChatterBox.Domain.Queries
{
    public class GetUserByNameQuery : IQuery<User>
    {
        private readonly string _name;

        public GetUserByNameQuery(string name)
        {
            _name = name;
        }

        public IQueryable<User> Execute(IQueryable<User> source)
        {
            return source.Where(x => x.Name == _name);
        }
    }
}