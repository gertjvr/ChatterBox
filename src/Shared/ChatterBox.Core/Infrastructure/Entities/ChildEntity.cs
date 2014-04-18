using System;
using ChatterBox.Core.Infrastructure.Facts;

namespace ChatterBox.Core.Infrastructure.Entities
{
    public abstract class ChildEntity<TParent> : Entity where TParent : Entity
    {
        private readonly TParent _parent;

        protected ChildEntity(TParent parent)
        {
            if (parent == null) 
                throw new ArgumentNullException("parent");

            _parent = parent;
        }

        public TParent Parent
        {
            get { return _parent; }
        }

        protected internal override void Append(IFact fact)
        {
            if (fact == null) 
                throw new ArgumentNullException("fact");

            Parent.Append(fact);
        }
    }
}