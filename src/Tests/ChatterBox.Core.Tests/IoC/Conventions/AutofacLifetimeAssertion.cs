using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Autofac.Core;
using ChatterBox.Core.Extensions;

namespace ChatterBox.Core.Tests.IoC.Conventions
{
    public class AutofacLifetimeAssertion : AutofacBaseAssertion
    {
        public override void Verify(IContainer container)
        {
            var exceptions = new List<Exception>();
            foreach (var registration in container.ComponentRegistry.Registrations)
            {
                var registrationLifetime = GetLifetime(registration);

                foreach (var ctorParameter in GetRegistrationCtorParameters(registration))
                {
                    IComponentRegistration parameterRegistration;
                    var typedService = new TypedService(ctorParameter.ParameterType);

                    // If the parameter is not registered with autofac, ignore
                    if (!container.ComponentRegistry.TryGetRegistration(typedService, out parameterRegistration)) continue;

                    var parameterLifetime = GetLifetime(parameterRegistration);

                    if (parameterLifetime >= registrationLifetime) continue;

                    var typeName = GetConcreteType(registration).ToTypeNameString();
                    var parameterType = ctorParameter.ParameterType.ToTypeNameString();

                    var error = string.Format("{0} ({1}) => {2} ({3})",
                        typeName, registrationLifetime,
                        parameterType, parameterLifetime);

                    exceptions.Add(new Exception(error));
                }
            }

            if (exceptions.Any())
                throw new AggregateException("Components should not depend on services with lesser lifetimes", exceptions);
        }
    }
}