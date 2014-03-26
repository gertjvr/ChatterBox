using System;
using System.Collections.Concurrent;
using ChatterBox.Core.Infrastructure.Entities;
using ChatterBox.Core.Infrastructure.Queries;
using ChatterBox.Core.Persistence;

namespace ChatterBox.Core.Infrastructure
{
    public class SourcererFactory
    {
        private readonly IFactStore _factStore;
        private readonly IDomainEventBroker _domainEventBroker;
        private readonly IClock _clock;
        private readonly IAggregateRebuilder _aggregateRebuilder;
        private readonly ConcurrentDictionary<Type, object> _snapshots;

        public SourcererFactory(IFactStore factStore, IDomainEventBroker domainEventBroker, IAggregateRebuilder aggregateRebuilder, IClock clock)
        {
            _factStore = factStore;
            _domainEventBroker = domainEventBroker;
            _aggregateRebuilder = aggregateRebuilder;
            _clock = clock;
            _snapshots = new ConcurrentDictionary<Type, object>();
        }

        public IUnitOfWork CreateUnitOfWork()
        {
            return new UnitOfWork(_factStore, _domainEventBroker, _clock);
        }

        public IRepository<TAggregateRoot> CreateRepository<TAggregateRoot>(IUnitOfWork unitOfWork) where TAggregateRoot : class, IAggregateRoot
        {
            return new Repository<TAggregateRoot>(GetOrCreateSnapshot<TAggregateRoot>(), unitOfWork);
        }

        private IQueryModel<TAggregateRoot> GetOrCreateSnapshot<TAggregateRoot>() where TAggregateRoot : class, IAggregateRoot
        {
            return (IQueryModel<TAggregateRoot>)_snapshots.GetOrAdd(typeof(TAggregateRoot), t => new QueryModel<TAggregateRoot>(_aggregateRebuilder));
        }     
    }
}