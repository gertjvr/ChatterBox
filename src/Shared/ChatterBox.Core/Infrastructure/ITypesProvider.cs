using System;

namespace ChatterBox.Core.Infrastructure
{
    public interface ITypesProvider
    {
        Type[] AggregateTypes { get; }
        Type[] FactTypes { get; }
    }
}