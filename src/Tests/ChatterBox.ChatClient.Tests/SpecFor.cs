using NUnit.Framework;

namespace ChatterBox.ChatClient.Tests
{
    [TestFixture]
    public abstract class SpecFor<T>
    {
        protected T Subject;

        protected abstract T Given();

        protected abstract void When();

        [SetUp]
        public void SetUp()
        {
            Subject = Given();
            When();
        }
    }

    public class ThenAttribute : TestAttribute
    {   
    }
}
