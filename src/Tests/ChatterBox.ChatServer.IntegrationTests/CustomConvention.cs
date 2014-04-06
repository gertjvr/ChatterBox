using System.Threading.Tasks;
using Fixie;
using Fixie.Conventions;

namespace ChatterBox.ChatServer.IntegrationTests
{
    public class CustomConvention : Convention
    {
        public CustomConvention()
        {
            Classes
                .Where(t => typeof(ISpecFor).IsAssignableFrom(t));

            Methods
                .Where(method => method.ReturnType.IsAssignableFrom(typeof(Task)));

            ClassExecution
                .CreateInstancePerTestClass();

            InstanceExecution
                .SetUpTearDown("FixtureSetUp", "FixtureTearDown");

            CaseExecution
                .SetUpTearDown("SetUp", "TearDown");
        }
    }
}