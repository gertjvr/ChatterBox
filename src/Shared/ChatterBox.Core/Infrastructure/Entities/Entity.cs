using System;
using ChatterBox.Core.Infrastructure.Facts;

namespace ChatterBox.Core.Infrastructure.Entities
{
    [Serializable]
    public abstract class Entity
    {
        public Guid Id { get; protected set; }
        protected internal abstract void Append(IFact fact);
    }
}