using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Autofac.Features.OwnedInstances;
using ChatterBox.Core.Infrastructure.Facts;
using ThirdDrawer.Extensions.CollectionExtensionMethods;

namespace ChatterBox.Core.Infrastructure
{
    public class DomainEventBroker : IDomainEventBroker
    {
        private readonly ILifetimeScope _lifetimeScope;

        public DomainEventBroker(ILifetimeScope lifetimeScope)
        {
            if (lifetimeScope == null) 
                throw new ArgumentNullException("lifetimeScope");

            _lifetimeScope = lifetimeScope;
        }

        public void Raise<T>(T fact) where T : IFact
        {
            if (fact == null)
                throw new ArgumentNullException("fact");

            Owned<IHandleFact<T>>[] handlers = null;
            try
            {
                handlers = _lifetimeScope.Resolve<IEnumerable<Owned<IHandleFact<T>>>>().ToArray();
                handlers.Do(h => h.Value.Handle(fact))
                        .Done();
            }
            finally
            {
                if (handlers != null)
                {
                    handlers.Do(h => h.Dispose())
                            .Done();
                }
            }
        }
    }
}