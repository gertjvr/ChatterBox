using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ChatterBox.Core.Tests.Fixie.Extensions;
using Fixie;
using Fixie.Conventions;

namespace ChatterBox.Core.Tests.Fixie.Conventions
{
    public class AllTypesConvention : Convention
    {
        static readonly string[] LifecycleMethods = { "FixtureSetUp", "FixtureTearDown", "SetUp", "TearDown" };

        public AllTypesConvention()
        {
            Classes
                .NameStartsWith("All");

            Methods
                .Where(method => method.IsVoid())
                .Where(method => LifecycleMethods.All(x => x != method.Name));

            Parameters(method =>
            {
                //Attempt to find a perfect matching source for N calls.
                var parameters = method.GetParameters();

                if (parameters.Length == 1)
                    return FindInputs(method.ReflectedType, parameters.Single().ParameterType);

                //No matching source method, so call it once with with zero parameters.
                return new[] { new object[] { } };
            });

            ClassExecution
                .CreateInstancePerTestClass();

            InstanceExecution
                .SetUpTearDown("FixtureSetUp", "FixtureTearDown");

            CaseExecution
                .SetUpTearDown("SetUp", "TearDown");
        }

        private static IEnumerable<object[]> FindInputs(Type testClass, Type parameterType)
        {
            var enumerableOfParameterType = typeof(IEnumerable<>).MakeGenericType(parameterType);

            var sources = testClass.GetMethods(BindingFlags.Static | BindingFlags.Public)
                                   .Where(m => !m.GetParameters().Any())
                                   .Where(m => m.ReturnType == enumerableOfParameterType)
                                   .ToArray();

            foreach (var source in sources)
                foreach (var input in (IEnumerable)source.Invoke(null, null))
                    yield return new[] { input };
        }
    }
}