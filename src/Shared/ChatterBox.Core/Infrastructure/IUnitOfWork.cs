using System;
using System.Collections.Generic;
using ChatterBox.Core.Infrastructure.Entities;

namespace ChatterBox.Core.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        void Enlist(IAggregateRoot item);
        IEnumerable<IAggregateRoot> EnlistedItems { get; }
        
        EventHandler<EventArgs> Completed { get; set; }
        void Complete();

        EventHandler<EventArgs> Abandoned { get; set; }
        void Abandon();
    }
}