using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Autofac.Core;
using ChatterBox.Core.Extensions;

namespace ChatterBox.Core.Tests.IoC.Conventions
{
    public class AutofacContainerAssertion : AutofacBaseAssertion
    {
        private readonly Func<Type, bool> _filter;
        private readonly Func<Type, bool> _isKnownOffender;

        public AutofacContainerAssertion()
            : this(type => true, type => true)
        {
        }

        public AutofacContainerAssertion(Func<Type, bool> filter, Func<Type, bool> isKnownOffender)
        {
            if (filter == null) 
                throw new ArgumentNullException("filter");
            
            if (isKnownOffender == null) 
                throw new ArgumentNullException("isKnownOffender");

            _filter = filter;
            _isKnownOffender = isKnownOffender;
        }

        public override void Verify(IContainer container)
        {
            var distinctTypes = container.ComponentRegistry.Registrations
                .SelectMany(r => r.Services.OfType<TypedService>().Select(s => s.ServiceType).Union(GetGenericFactoryTypes(r)))
                .Where(t => _filter(t))
                .Distinct();

            var exceptions = new List<Exception>();
            foreach (var distinctType in distinctTypes)
            {
                if (_isKnownOffender(distinctType))
                {
                    Console.WriteLine("'{0}' is known not to resolve correctly - ignoring it.", distinctType.ToTypeNameString());
                    return;
                }

                try
                {
                    container.Resolve(distinctType);
                }
                catch (DependencyResolutionException e)
                {
                    if (e.Message.Contains("No scope with a Tag"))
                    {
                        Console.WriteLine("'{0}' failed to resolve 'No scope with a Tag' - The test is considered inconclusive.", distinctType.ToTypeNameString());
                        return;
                    }

                    exceptions.Add(e);
                }
            }

            if (exceptions.Any())
                throw new AggregateException("Can't resolve all types registered with Autofac", exceptions);
        }
    }
}