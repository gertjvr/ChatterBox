using System;
using System.Collections.Generic;
using ChatterBox.Core.Infrastructure.Facts;

namespace ChatterBox.Core.Infrastructure.Entities
{
    public abstract class AggregateRoot : Entity, IAggregateRoot
    {
        private readonly List<IFact> _pendingFacts = new List<IFact>();

        protected internal override void Append(IFact fact)
        {
            if (fact == null) 
                throw new ArgumentNullException("fact");

            lock (_pendingFacts)
            {
                _pendingFacts.Add(fact);
            }
        }

        public IEnumerable<IFact> GetAndClearPendingFacts()
        {
            lock (_pendingFacts)
            {
                var facts = _pendingFacts.ToArray();
                _pendingFacts.Clear();
                return facts;
            }
        }

        public Guid RevisionId { get; private set; }

        public void SetRevisionId(Guid revisionId)
        {
            if (revisionId == Guid.Empty) 
                throw new ArgumentException("Guid cannot be empty.", "revisionId");

            RevisionId = revisionId;
        }
        
    }
}