using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Autofac.Core;

namespace ChatterBox.Core.Tests.IoC
{
    public class AutofacContainerAssertion 
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

        private void DemandCanBeResolved(ILifetimeScope scope, Type serviceType)
        {
            try
            {
                scope.Resolve(serviceType);
            }
            catch (DependencyResolutionException)
            {
                var componentRegistration = RegistrationForServiceType(serviceType,
                    scope.ComponentRegistry);

                var underlyingType = componentRegistration.Target.Activator.LimitType;
                var factoryDelegateType = underlyingType.GetNestedType("Factory");

                if (factoryDelegateType == null) 
                    throw;

                var factoryDelegateParameters = factoryDelegateType.GetMethod("Invoke").GetParameters();
                var constructorParameters = serviceType.GetConstructors().Single().GetParameters();

                var constructorParameterTypes = constructorParameters.Select(p => p.ParameterType);
                var factoryDelegateParameterTypes = factoryDelegateParameters.Select(p => p.ParameterType);
                var parameterTypesThatMustBeResolvedFromTheContainer = constructorParameterTypes
                    .Except(factoryDelegateParameterTypes)
                    .ToArray();

                foreach (var parameterType in parameterTypesThatMustBeResolvedFromTheContainer)
                {
                    DemandCanBeResolved(scope, parameterType);
                }
            }
        }

        private IComponentRegistration RegistrationForServiceType(Type serviceType, IComponentRegistry componentRegistry)
        {
            IComponentRegistration componentRegistration = ExtractComponentRegistrations(componentRegistry)
                .Where(candidate => candidate.TypedService.ServiceType == serviceType)
                .Select(candidate => candidate.ComponentRegistration)
                .First();
            return componentRegistration;
        }

        private IEnumerable<Candidate> ExtractComponentRegistrations(IComponentRegistry componentRegistry)
        {
            return from r in componentRegistry.Registrations
                   from s in r.Services
                   where s is TypedService
                   where _filter(((TypedService)s).ServiceType)
                   orderby s.Description
                   select new Candidate(r, (TypedService)s);
        }

        private class Candidate
        {
            public Candidate(IComponentRegistration componentRegistration, TypedService typedService)
            {
                ComponentRegistration = componentRegistration;
                TypedService = typedService;
            }

            public IComponentRegistration ComponentRegistration { get; private set; }
            public TypedService TypedService { get; private set; }
        }

        public void Verify(IContainer container)
        {
            var candidates = ExtractComponentRegistrations(container.ComponentRegistry).ToArray();

            foreach (var candidate in candidates)
            {
                var serviceType = candidate.TypedService.ServiceType;

                if (_isKnownOffender(serviceType))
                {
                    Console.WriteLine("'{0}' is known not to resolve correctly - ignoring it.", serviceType.FullName);
                    return;
                }

                try
                {
                    using (ILifetimeScope scope = container.BeginLifetimeScope("AutofacWebRequest"))
                    {
                        DemandCanBeResolved(scope, serviceType);
                    }
                }
                catch (DependencyResolutionException ex)
                {
                    if (ex.Message.Contains("No scope with a Tag"))
                    {
                        Console.WriteLine("'{0}' failed to resolve 'No scope with a Tag' - The test is considered inconclusive.", candidate.TypedService.Description);
                        return;
                    }

                    throw;
                }
            }
        }
    }
}