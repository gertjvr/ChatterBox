using System.Threading.Tasks;

namespace ChatterBox.ChatServer.IntegrationTests
{
    public abstract class SpecFor<T> : ISpecFor
    {
        protected T Subject;

        protected abstract Task<T> Given();

        protected abstract Task When();

        public async Task SetUp()
        {
            Subject = await Given();
            await When();
        }
    }
}
