using System;
using System.Collections.Generic;
using ChatterBox.Core.Infrastructure.Facts;

namespace ChatterBox.Core.Infrastructure.Entities
{
    public interface IAggregateRoot
    {
        Guid Id { get; }
        IEnumerable<IFact> GetAndClearPendingFacts();
        Guid RevisionId { get; set; }
    }
}