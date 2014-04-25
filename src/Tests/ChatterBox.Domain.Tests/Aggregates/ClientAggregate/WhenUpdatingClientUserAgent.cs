using ChatterBox.Core.Tests.Specifications;
using ChatterBox.Domain.Aggregates.ClientAggregate;
using Ploeh.AutoFixture;
using Shouldly;

namespace ChatterBox.Domain.Tests.Aggregates.ClientAggregate
{
    public class WhenUpdatingClientUserAgent : AutoSpecificationFor<Client>
    {
        public string NewUserAgent { get; private set; }

        protected override Client Given()
        {
            NewUserAgent = Fixture.Create<string>();

            return Fixture.Create<Client>();
        }

        protected override void When()
        {
            Subject.UpdateUserAgent(NewUserAgent);
        }

        [Then]
        public void ShouldHaveUpdatedClientUserAgent()
        {
            Subject.UserAgent.ShouldBe(NewUserAgent);
        }
    }
}