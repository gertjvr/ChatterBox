using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Autofac.Core;
using NUnit.Framework;

namespace ChatterBox.ChatClient.Tests.Conventions
{
    [TestFixture]
    public abstract class AllTypesRegisteredWithTheContainer
    {
        private IContainer _container;

        [Test]
        [TestCaseSource("TestCases")]
        public void CanBeResolved(IComponentRegistration componentRegistration, TypedService typedService)
        {
            var serviceType = typedService.ServiceType;
            if (IsKnownOffender(serviceType)) Assert.Ignore("Fix these as you find them.");

            Console.WriteLine(componentRegistration);
            Console.WriteLine(typedService);

            try
            {
                using (var scope = _container.BeginLifetimeScope("AutofacWebRequest"))
                {
                    DemandCanBeResolved(scope, serviceType);
                }
            }
            catch (DependencyResolutionException exc)
            {
                if (exc.Message.Contains("No scope with a Tag")) Assert.Inconclusive();
                throw;
            }
        }

        private void DemandCanBeResolved(ILifetimeScope scope, Type serviceType)
        {
            try
            {
                scope.Resolve(serviceType);
            }
            catch (DependencyResolutionException)
            {
                var componentRegistration = RegistrationForServiceType(serviceType, scope.ComponentRegistry);

                var underlyingType = componentRegistration.Target.Activator.LimitType;
                var factoryDelegateType = underlyingType.GetNestedType("Factory");

                if (factoryDelegateType == null) throw;

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
            Console.WriteLine("Searching for registration for service type {0}", serviceType);

            var componentRegistration = ExtractComponentRegistrations(componentRegistry)
                .Where(tuple => tuple.Item2.ServiceType == serviceType)
                .Select(tuple => tuple.Item1)
                .First();
            return componentRegistration;
        }

        protected abstract IContainer CreateContainer();
        protected abstract bool Filter(Type serviceType);
        protected abstract bool IsKnownOffender(Type serviceType);

        private IEnumerable<Tuple<IComponentRegistration, TypedService>> ExtractComponentRegistrations(IComponentRegistry componentRegistry)
        {
            return from r in componentRegistry.Registrations
                   from s in r.Services
                   where s is TypedService
                   where Filter(((TypedService)s).ServiceType)
                   orderby s.Description
                   select new Tuple<IComponentRegistration, TypedService>(r, (TypedService)s);
        }

        public IEnumerable<TestCaseData> TestCases()
        {
            if (_container == null) _container = CreateContainer();

            var testCases = ExtractComponentRegistrations(_container.ComponentRegistry)
                .Select(tuple => new TestCaseData(tuple.Item1, tuple.Item2).SetName(tuple.Item2.Description))
                .ToArray();
            return testCases;
        }

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            if (_container == null) _container = CreateContainer();
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            var container = _container;
            if (container == null) return;

            container.Dispose();
        }
    }
}