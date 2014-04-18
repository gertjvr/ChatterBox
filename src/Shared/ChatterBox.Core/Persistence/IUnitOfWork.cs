using System;
using System.Collections.Generic;
using ChatterBox.Core.Infrastructure.Entities;

namespace ChatterBox.Core.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        void Enlist(IAggregateRoot item);
        IEnumerable<IAggregateRoot> EnlistedItems { get; }
        IRepository<T> Repository<T>() where T : IAggregateRoot;

        EventHandler<EventArgs> Completed { get; set; }
        void Complete();

        EventHandler<EventArgs> Abandoned { get; set; }
        void Abandon();
    }
}