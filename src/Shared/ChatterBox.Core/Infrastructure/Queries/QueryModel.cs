using System;
using System.CodeDom.Compiler;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using ChatterBox.Core.Infrastructure.Entities;
using ThirdDrawer.Extensions.CollectionExtensionMethods;

namespace ChatterBox.Core.Infrastructure.Queries
{
    public class QueryModel<T> : IQueryModel<T> where T : class, IAggregateRoot
    {
        private readonly IAggregateRebuilder _aggregateRebuilder;
        private readonly Lazy<ConcurrentDictionary<Guid, T>> _items;

        private readonly ConcurrentDictionary<Guid, Guid> _lastSeenRevisionIds = new ConcurrentDictionary<Guid, Guid>();

        public QueryModel(IAggregateRebuilder aggregateRebuilder)
        {
            if (aggregateRebuilder == null) 
                throw new ArgumentNullException("aggregateRebuilder");

            _aggregateRebuilder = aggregateRebuilder;
            _items = new Lazy<ConcurrentDictionary<Guid, T>>(RebuildAll);
        }

        private ConcurrentDictionary<Guid, T> RebuildAll()
        {
            return new ConcurrentDictionary<Guid, T>(_aggregateRebuilder.RebuildAll<T>().ToDictionary(a => a.Id, a => a));
        }

        public T GetById(Guid itemId)
        {
            if (itemId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "itemId");

            return _items.Value[itemId];
        }

        public IQueryable<T> Items
        {
            get { return _items.Value.Values.AsQueryable(); }
        }

        public void Add(T item)
        {
            if (item == null) 
                throw new ArgumentNullException("item");

            _items.Value.TryAdd(item.Id, item);
        }

        public void Remove(T item)
        {
            if (item == null) 
                throw new ArgumentNullException("item");

            T dummy;
            _items.Value.TryRemove(item.Id, out dummy);
        }

        public void Revert(IEnumerable<T> newItems, IEnumerable<T> modifiedItems, IEnumerable<T> removedItems)
        {
            if (newItems == null) 
                throw new ArgumentNullException("newItems");
            
            if (modifiedItems == null) 
                throw new ArgumentNullException("modifiedItems");
            
            if (removedItems == null) 
                throw new ArgumentNullException("removedItems");

            lock (this)
            {
                T dummy;
                newItems.AsParallel()
                        .Do(item => _items.Value.TryRemove(item.Id, out dummy))
                        .Done();

                var actualModifiedItems = modifiedItems
                    .Where(item => item.RevisionId != LastSeenRevisionId(item.Id))
                    .ToArray();

                var itemsToRebuild = removedItems.Union(actualModifiedItems);

                itemsToRebuild
                    .AsParallel()
                    .Select(itemToRebuild => _aggregateRebuilder.Rebuild<T>(itemToRebuild.Id))
                    .Do(item => _items.Value[item.Id] = item)
                    .Do(item => _lastSeenRevisionIds[item.Id] = item.RevisionId)
                    .Done();
            }
        }

        private Guid LastSeenRevisionId(Guid itemId)
        {
            if (itemId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "itemId");

            Guid lastSeenRevisionId;
            _lastSeenRevisionIds.TryGetValue(itemId, out lastSeenRevisionId);
            return lastSeenRevisionId;
        }
    }
}