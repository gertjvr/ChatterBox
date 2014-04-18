using System;
using System.Linq;

namespace ChatterBox.Core.Infrastructure.Queries
{
    public interface IQueryContext<TAggregateRoot> : IDisposable
    {
        TProjection Query<TProjection>(Func<IQueryable<TAggregateRoot>, TProjection> query);
    }
}