using System;
using System.Linq;
using ChatterBox.Core.Infrastructure.Entities;

namespace ChatterBox.Core.Infrastructure
{
    public interface IRepository<TAggregateRoot> where TAggregateRoot : IAggregateRoot
    {
        TAggregateRoot GetById(Guid id);
        void Add(TAggregateRoot item);
        void Remove(TAggregateRoot item);

        TAggregateRoot[] Query(Func<IQueryable<TAggregateRoot>, TAggregateRoot[]> query);
        TProjection Query<TProjection>(Func<IQueryable<TAggregateRoot>, TProjection> query);
    }
}