namespace ChatterBox.Core.Tests.Specifications
{
    public abstract class SpecificationFor<T> : ISpecificationFor
        where T : class
    {
        protected T Subject;

        protected abstract T Given();

        protected abstract void When();

        public virtual void SetUp()
        {
            Subject = Given();
            When();
        }

        public virtual void TearDown()
        {
            Subject = null;
        }
    }
}
