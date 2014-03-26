using System;

namespace ChatterBox.Core.Persistence
{
    public interface ITypesProvider
    {
        Type[] AggregateTypes { get; }
        Type[] FactTypes { get; }
    }
}