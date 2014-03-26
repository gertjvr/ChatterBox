using System;
using ChatterBox.Core.Infrastructure.Entities;
using ChatterBox.Core.Infrastructure.Queries;

namespace ChatterBox.Core.Persistence
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        T GetById(Guid id);
        void Add(T item);
        void Remove(T item);

        T[] Query(IQuery<T> query);
        TProjection Query<TProjection>(IQuery<T, TProjection> query);
    }
}