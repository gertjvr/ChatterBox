using System.Linq;
using ChatterBox.Core.Infrastructure.Entities;

namespace ChatterBox.Core.Infrastructure.Queries
{
    public interface IQuery<TEntity>
    {
        IQueryable<TEntity> Execute(IQueryable<TEntity> source);
    }

    public interface IQuery<TEntity, TProjection> where TEntity : IAggregateRoot
    {
        TProjection Execute(IQueryable<TEntity> source);
    }
}