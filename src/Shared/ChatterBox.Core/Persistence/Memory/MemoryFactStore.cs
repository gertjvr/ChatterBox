using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using ChatterBox.Core.Infrastructure;
using ChatterBox.Core.Infrastructure.Entities;
using ChatterBox.Core.Infrastructure.Facts;

namespace ChatterBox.Core.Persistence.Memory
{
    public class MemoryFactStore : IFactStore
    {
        private readonly ConcurrentDictionary<Guid, SortedList<UnitOfWorkProperties, IFact>> _streams = new ConcurrentDictionary<Guid, SortedList<UnitOfWorkProperties, IFact>>();
        private readonly object _mutex = new object();

        public void AppendAtomically(IFact[] facts)
        {
            if (facts == null) 
                throw new ArgumentNullException("facts");

            lock (_mutex)
            {
                var aggregateRoots = facts.GroupBy(f => f.AggregateRootId);
                foreach (var grouping in aggregateRoots)
                {
                    var stream = _streams.GetOrAdd(grouping.Key, x => new SortedList<UnitOfWorkProperties, IFact>());
                    foreach (var fact in grouping)
                    {
                        stream.Add(fact.UnitOfWorkProperties, fact);
                    }
                }
            }
        }

        public IEnumerable<FactAbout<T>> GetStream<T>(Guid id) where T : IAggregateRoot
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "id");

            var stream = _streams.GetOrAdd(id, x => new SortedList<UnitOfWorkProperties, IFact>());

            lock (_mutex)
            {
                var facts = stream
                    .Values
                    .Cast<FactAbout<T>>()
                    .ToArray();
                return facts;
            }
        }

        public IEnumerable<Guid> GetAllStreamIds<T>() where T : IAggregateRoot
        {
            foreach (var aggregateRootId in _streams.Keys)
            {
                var stream = _streams.GetOrAdd(aggregateRootId, x => new SortedList<UnitOfWorkProperties, IFact>());
                var firstFact = stream.First();
                var aggregateRootType = Type.GetType(firstFact.Value.EntityTypeName);

                if (typeof (T).IsAssignableFrom(aggregateRootType)) yield return aggregateRootId;
            }
        }

        public virtual IEnumerable<IGrouping<Guid, IFact>> GetAllFactsGroupedByUnitOfWork()
        {
            return _streams.Values
                           .SelectMany(stream => stream)
                           .OrderBy(kvp => kvp.Key)
                           .Select(kvp => kvp.Value)
                           .GroupBy(f => f.UnitOfWorkProperties.UnitOfWorkId)
                ;
        }

        public virtual void ImportFrom(IEnumerable<IFact> facts)
        {
            if (facts == null) 
                throw new ArgumentNullException("facts");

            foreach (var fact in facts)
            {
                var stream = _streams.GetOrAdd(fact.AggregateRootId, x => new SortedList<UnitOfWorkProperties, IFact>());
                stream.Add(fact.UnitOfWorkProperties, fact);
            }
        }

        public bool HasFacts
        {
            get { return GetAllFactsGroupedByUnitOfWork().Any(); }
        }
    }
}