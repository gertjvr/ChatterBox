using System.Linq;
using ChatterBox.Core.Infrastructure.Queries;
using ChatterBox.Domain.Aggregates.UserAggregate;

namespace ChatterBox.Domain.Queries
{
    public class EnsureUserNameIsAvailibleQuery : IQuery<User>
    {
        private readonly string _userName;

        public EnsureUserNameIsAvailibleQuery(string userName)
        {
            _userName = userName;
        }

        public IQueryable<User> Execute(IQueryable<User> source)
        {
            return source.Where(x => x.Name == _userName);
        }
    }
}