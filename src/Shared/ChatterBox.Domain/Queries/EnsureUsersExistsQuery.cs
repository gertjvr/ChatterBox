using System.Linq;
using ChatterBox.Core.Infrastructure.Queries;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ThirdDrawer.Extensions.CollectionExtensionMethods;

namespace ChatterBox.Domain.Queries
{
    public class EnsureUsersExistsQuery : IQuery<User, bool>
    {
        public bool Execute(IQueryable<User> source)
        {
            return source.None();
        }
    }
}