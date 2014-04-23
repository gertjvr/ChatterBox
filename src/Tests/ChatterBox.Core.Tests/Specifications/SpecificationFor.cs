using NUnit.Framework;

namespace ChatterBox.Core.Tests.Specifications
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

        [TearDown]
        public virtual void TearDown()
        {
            Subject = null;
        }
    }
}
