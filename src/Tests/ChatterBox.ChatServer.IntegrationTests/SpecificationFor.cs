using NUnit.Framework;

namespace ChatterBox.ChatServer.IntegrationTests
{
    [TestFixture]
    public abstract class SpecificationFor<T>
        where T : class
    {
        protected T Subject;

        protected abstract T Given();

        protected abstract void When();

        [SetUp]
        public virtual void SetUp()
        {
            Subject = Given();
            When();
        }
    }
}
