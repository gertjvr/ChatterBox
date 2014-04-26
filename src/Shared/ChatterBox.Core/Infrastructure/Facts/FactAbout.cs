using System;
using ChatterBox.Core.Infrastructure.Entities;

namespace ChatterBox.Core.Infrastructure.Facts
{
    public abstract class FactAbout<TAggregateRoot> : IFact where TAggregateRoot : IAggregateRoot
    {
        private UnitOfWorkProperties _unitOfWorkProperties;
        
        protected FactAbout(Guid aggregateRootId)
        {
            if (aggregateRootId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "aggregateRootId");

            AggregateRootId = aggregateRootId;
        } 

        public Guid AggregateRootId { get; private set; }

        /// <remarks>
        ///     This will be set by our fact store at commit time.
        /// </remarks>
        public UnitOfWorkProperties UnitOfWorkProperties
        {
            get { return _unitOfWorkProperties; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");

                _unitOfWorkProperties = value;
            }
        }

        public string StreamName
        {
            get { return typeof (TAggregateRoot).Name; }
        }

        public string EntityTypeName
        {
            get { return typeof (TAggregateRoot).AssemblyQualifiedName; }
        }
    }
}