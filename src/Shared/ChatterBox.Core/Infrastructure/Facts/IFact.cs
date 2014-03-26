using System;
using ChatterBox.Core.Persistence;

namespace ChatterBox.Core.Infrastructure.Facts
{
    public interface IFact
    {
        Guid AggregateRootId { get; }
        string StreamName { get; }
        string EntityTypeName { get; }

        UnitOfWorkProperties UnitOfWorkProperties { get; }
        void SetUnitOfWorkProperties(UnitOfWorkProperties properties);
    }
}