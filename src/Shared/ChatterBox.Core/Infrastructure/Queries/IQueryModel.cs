using System;
using System.Collections.Generic;
using System.Linq;
using ChatterBox.Core.Infrastructure.Entities;

namespace ChatterBox.Core.Infrastructure.Queries
{
    public interface IQueryModel<T> where T : class, IAggregateRoot
    {
        T GetById(Guid id);

        IQueryable<T> Items { get; }

        void UpdateAtomically(IEnumerable<T> newItems, IEnumerable<T> modifiedItems, IEnumerable<T> removedItems);
    }
}