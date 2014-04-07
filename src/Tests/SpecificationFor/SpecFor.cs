using NUnit.Framework;

namespace SpecificationFor
{
    [TestFixture]
    public abstract class SpecFor<T> : ISpecFor
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
