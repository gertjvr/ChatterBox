using System;
using ChatterBox.Core.Infrastructure.Entities;

namespace ChatterBox.Core.Infrastructure.Facts
{
    public abstract class FactAbout<T> : IFact where T : IAggregateRoot
    {
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
        public UnitOfWorkProperties UnitOfWorkProperties { get; private set; }

        public string StreamName
        {
            get { return typeof (T).Name; }
        }

        public string EntityTypeName
        {
            get { return typeof (T).AssemblyQualifiedName; }
        }

        public void SetUnitOfWorkProperties(UnitOfWorkProperties properties)
        {
            if (properties == null) 
                throw new ArgumentNullException("properties");

            UnitOfWorkProperties = properties;
        }
    }
}