using System;
using ChatterBox.Core.Infrastructure.Entities;
using ChatterBox.Core.Persistence;

namespace ChatterBox.Core.Infrastructure.Facts
{
    public abstract class FactAbout<T> : IFact where T : IAggregateRoot
    {
        public Guid AggregateRootId { get; set; }

        /// <remarks>
        ///     This will be set by our fact store at commit time.
        /// </remarks>
        public UnitOfWorkProperties UnitOfWorkProperties { get; set; }

        protected FactAbout()
        {
        }

        protected FactAbout(Guid aggregateRootId)
        {
            AggregateRootId = aggregateRootId;
        }

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
            UnitOfWorkProperties = properties;
        }
    }
}