using System;
using ChatterBox.Core.Infrastructure.Entities;

namespace ChatterBox.Core.Infrastructure
{
    public interface IAggregateRebuilder
    {
        T Rebuild<T>(Guid id) where T : class, IAggregateRoot;
        T[] RebuildAll<T>() where T : class, IAggregateRoot;
    }
}