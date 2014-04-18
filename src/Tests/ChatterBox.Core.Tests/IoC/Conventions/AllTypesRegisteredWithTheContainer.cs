using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Core;
using Shouldly;
using ThirdDrawer.Extensions.StringExtensionMethods;

namespace ChatterBox.Core.Tests.IoC.Conventions
{
    public abstract class AllTypesRegisteredWithTheContainer : IDisposable
    {
        private readonly IContainer _container;

        public AllTypesRegisteredWithTheContainer(IContainer container)
        {
            _container = container;
        }

        public void Dispose()
        {
            IContainer container = _container;
            if (container == null) return;

            container.Dispose();
        }

        protected abstract bool Filter(Type serviceType);
        protected abstract bool IsKnownOffender(Type serviceType);

        public void CanBeResolved()
        {
            var candidates = ExtractComponentRegistrations(_container.ComponentRegistry).ToArray();

            var failed = new List<Candidate>();
            var ignored = new List<Candidate>();
            var inconclusive = new List<Candidate>();

            foreach (var candidate in candidates)
            {

                Type serviceType = candidate.TypedService.ServiceType;
                if (IsKnownOffender(serviceType))
                {
                    ignored.Add(candidate);
                    Console.WriteLine("'{0}' is known not to resolve correctly - ignoring it."
                        .FormatWith(serviceType.FullName));
                    return;
                }

                Console.WriteLine(candidate.ComponentRegistration);
                Console.WriteLine(candidate.TypedService);

                try
                {
                    using (ILifetimeScope scope = _container.BeginLifetimeScope("AutofacWebRequest"))
                    {
                        DemandCanBeResolved(scope, serviceType);
                    }
                }
                catch (DependencyResolutionException exc)
                {
                    if (exc.Message.Contains("No scope with a Tag"))
                    {
                        inconclusive.Add(candidate);
                        Console.WriteLine("'{0}' failed to resolve 'No scope with a Tag' - The test is considered inconclusive."
                            .FormatWith(candidate.TypedService.Description));
                        return;
                    }

                    failed.Add(candidate);
                }
            }

            var succeeded = candidates.Count() - failed.Count - ignored.Count - inconclusive.Count;
            Console.WriteLine("{0} resolved successfully, {1} failed, {2} were ignored, {3} were inconclusive.".FormatWith(succeeded, failed.Count, ignored.Count, inconclusive.Count));

            failed.Select(c => c.TypedService.Description).ShouldBeEmpty();
        }

        private void DemandCanBeResolved(ILifetimeScope scope, Type serviceType)
        {
            try
            {
                scope.Resolve(serviceType);
            }
            catch (DependencyResolutionException)
            {
                IComponentRegistration componentRegistration = RegistrationForServiceType(serviceType,
                    scope.ComponentRegistry);

                Type underlyingType = componentRegistration.Target.Activator.LimitType;
                Type factoryDelegateType = underlyingType.GetNestedType("Factory");

                if (factoryDelegateType == null) throw;

                ParameterInfo[] factoryDelegateParameters = factoryDelegateType.GetMethod("Invoke").GetParameters();
                ParameterInfo[] constructorParameters = serviceType.GetConstructors().Single().GetParameters();

                IEnumerable<Type> constructorParameterTypes = constructorParameters.Select(p => p.ParameterType);
                IEnumerable<Type> factoryDelegateParameterTypes = factoryDelegateParameters.Select(p => p.ParameterType);
                Type[] parameterTypesThatMustBeResolvedFromTheContainer = constructorParameterTypes
                    .Except(factoryDelegateParameterTypes)
                    .ToArray();

                foreach (Type parameterType in parameterTypesThatMustBeResolvedFromTheContainer)
                {
                    DemandCanBeResolved(scope, parameterType);
                }
            }
        }

        private IComponentRegistration RegistrationForServiceType(Type serviceType, IComponentRegistry componentRegistry)
        {
            Console.WriteLine("Searching for registration for service type {0}", serviceType);

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
                where Filter(((TypedService) s).ServiceType)
                orderby s.Description
                select new Candidate(r, (TypedService) s);
        }

        public class Candidate
        {
            public Candidate(IComponentRegistration componentRegistration, TypedService typedService)
            {
                ComponentRegistration = componentRegistration;
                TypedService = typedService;
            }

            public IComponentRegistration ComponentRegistration { get; private set; }
            public TypedService TypedService { get; private set; }
        }
    }
}