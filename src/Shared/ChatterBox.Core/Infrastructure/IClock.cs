using System;

namespace ChatterBox.Core.Infrastructure
{
    public interface IClock
    {
        DateTimeOffset UtcNow { get; }
    }
}