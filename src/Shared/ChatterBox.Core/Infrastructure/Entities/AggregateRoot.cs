using System;
using System.Collections.Generic;
using ChatterBox.Core.Infrastructure.Facts;
using Newtonsoft.Json;

namespace ChatterBox.Core.Infrastructure.Entities
{
    public abstract class AggregateRoot : Entity, IAggregateRoot
    {
        private readonly List<IFact> _pendingFacts = new List<IFact>();

        protected internal override void Append(IFact fact)
        {
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

        public Guid RevisionId { get; set; }

        public T Clone<T>() where T : class, IAggregateRoot
        {
            var serialze = JsonConvert.SerializeObject(this);
            var clone = JsonConvert.DeserializeObject<T>(serialze);
            return clone;
        }
    }
}