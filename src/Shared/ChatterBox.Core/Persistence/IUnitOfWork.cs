using System;
using ChatterBox.Core.Infrastructure.Entities;

namespace ChatterBox.Core.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        void Enlist(IAggregateRoot item);

        IRepository<TAggregateRoot> Repository<TAggregateRoot>() where TAggregateRoot : IAggregateRoot; 
        
        EventHandler<EventArgs> Completed { get; set; }
        void Complete();

        EventHandler<EventArgs> Abandoned { get; set; }
        void Abandon();
    }
}