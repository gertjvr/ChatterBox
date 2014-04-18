using System;
using System.Linq;
using ChatterBox.Core.Infrastructure.Entities;

namespace ChatterBox.Core.Infrastructure.Queries
{
    internal class QueryContext<TAggregateRoot> : IQueryContext<TAggregateRoot> where TAggregateRoot : class, IAggregateRoot
    {
        private readonly IQueryModel<TAggregateRoot> _queryModel;
        private bool _disposed;

        public QueryContext(IQueryModel<TAggregateRoot> queryModel)
        {
            _queryModel = queryModel;

            DomainOperationMutex.Wait();
        }

        public TProjection Query<TProjection>(Func<IQueryable<TAggregateRoot>, TProjection> query)
        {
            return query(_queryModel.Items);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;
            if (_disposed) return;

            try
            {
                DomainOperationMutex.Release();
            }
            finally
            {
                _disposed = true;
            }
        }
    }
}