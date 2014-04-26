﻿using System;
using System.Collections.Generic;
using System.Linq;
using ChatterBox.Core.Infrastructure.Entities;
using ChatterBox.Core.Infrastructure.Facts;
using ThirdDrawer.Extensions.CollectionExtensionMethods;

namespace ChatterBox.Core.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IFactStore _factStore;
        private readonly IDomainEventBroker _domainEventBroker;
        private readonly List<IAggregateRoot> _enlistedItems = new List<IAggregateRoot>();
        private readonly IClock _clock;
        private bool _completed;
        private bool _abandoned;
        private bool _disposed;

        public UnitOfWork(IFactStore factStore, IDomainEventBroker domainEventBroker, IClock clock)
        {
            if (factStore == null) 
                throw new ArgumentNullException("factStore");
            
            if (domainEventBroker == null) 
                throw new ArgumentNullException("domainEventBroker");
            
            if (clock == null) 
                throw new ArgumentNullException("clock");
           

            _factStore = factStore;
            _domainEventBroker = domainEventBroker;
            _clock = clock;

            DomainOperationMutex.Wait();
        }

        public void Enlist(IAggregateRoot item)
        {
            if (item == null) 
                throw new ArgumentNullException("item");

            _enlistedItems.Add(item);
        }

        public IEnumerable<IAggregateRoot> EnlistedItems
        {
            get { return _enlistedItems; }
        }

        public EventHandler<EventArgs> Completed { get; set; }

        public void Complete()
        {
            if (_abandoned) throw new InvalidOperationException();

            var unitOfWorkId = Guid.NewGuid();
            var sequenceNumber = 0;

            var facts = new List<IFact>();
            while (true)
            {
                var factsFromThisPass = _enlistedItems
                    .SelectMany(item => item.GetAndClearPendingFacts())
                    .ToArray();

                if (factsFromThisPass.None()) break;

                facts.AddRange(factsFromThisPass);
                foreach (var fact in factsFromThisPass)
                {
                    fact.UnitOfWorkProperties = new UnitOfWorkProperties(unitOfWorkId, sequenceNumber, _clock.UtcNow);
                    _domainEventBroker.Raise((dynamic)fact);

                    sequenceNumber++;
                }
            }

            var factsArray = facts.ToArray();
            _factStore.AppendAtomically(factsArray);
            _enlistedItems.Do(ar => ar.SetRevisionId(unitOfWorkId)).Done();

            _completed = true;

            var completedHandler = Completed;
            if (completedHandler == null) return;
            completedHandler(this, EventArgs.Empty);
        }

        public EventHandler<EventArgs> Abandoned { get; set; }

        public void Abandon()
        {
            if (_completed) throw new InvalidOperationException();

            _abandoned = true;

            var abandonedHandler = Abandoned;
            if (abandonedHandler == null) return;
            abandonedHandler(this, EventArgs.Empty);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;
            if (_disposed) return;

            try
            {
                if (!_completed) Abandon();
            }
            finally
            {
                _disposed = true;
                DomainOperationMutex.Release();
            }
        }
    }
}