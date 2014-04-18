using System.Linq;
using System.Threading.Tasks;
using ChatterBox.Core.Tests.Fixie.Extensions;
using ChatterBox.Core.Tests.Specifications;
using Fixie;
using Fixie.Conventions;
using ThirdDrawer.Extensions.TypeExtensionMethods;

namespace ChatterBox.Core.Tests.Fixie.Conventions
{
    public class SpecificationForConvention : Convention
    {
        static readonly string[] LifecycleMethods = { "FixtureSetUp", "FixtureTearDown", "SetUp", "TearDown", "When" };

        public SpecificationForConvention()
        {
            Classes
                .Where(type => type.IsInNamespace(GetType().Namespace))
                .Where(type => type.IsAssignableTo<ISpecificationFor>());

            Methods
                .Where(method => method.IsVoid() || method.ReturnType.IsAssignableTo<Task>())
                .Where(method => LifecycleMethods.All(x => x != method.Name));

            ClassExecution
                .CreateInstancePerCase();

            InstanceExecution
                .SetUpTearDown("FixtureSetUp", "FixtureTearDown");

            CaseExecution
                .SetUpTearDown("SetUp", "TearDown");
        }
    }
}