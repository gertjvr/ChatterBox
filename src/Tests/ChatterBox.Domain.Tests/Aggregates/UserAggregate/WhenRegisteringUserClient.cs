using ChatterBox.Core.Tests.Specifications;
using ChatterBox.Domain.Aggregates.ClientAggregate;
using ChatterBox.Domain.Aggregates.UserAggregate;
using Ploeh.AutoFixture;
using Shouldly;

namespace ChatterBox.Domain.Tests.Aggregates.UserAggregate
{
    public class WhenRegisteringUserClient : AutoSpecificationFor<User>
    {
        public Client Client { get; private set; }

        protected override User Given()
        {
            Client = Fixture.Create<Client>();

            return Fixture.Create<User>();
        }

        protected override void When()
        {
            Subject.RegisterClient(Client);
        }

        [Then]
        public void ShouldHaveRegisteredUserClient()
        {
            Subject.ConnectedClients.ShouldContain(Client.Id);
        }
    }
}