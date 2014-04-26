using System;

namespace ChatterBox.Core.Infrastructure.Facts
{
    public interface IFact
    {
        Guid AggregateRootId { get; }
        string StreamName { get; }
        string EntityTypeName { get; }

        UnitOfWorkProperties UnitOfWorkProperties { get; set; }
    }
}