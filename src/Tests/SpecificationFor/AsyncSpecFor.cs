using System.Threading.Tasks;
using NUnit.Framework;

namespace SpecificationFor
{
    [TestFixture]
    public abstract class AsyncSpecFor<T> : ISpecFor
    {
        protected T Subject;

        protected abstract Task<T> Given();

        protected abstract Task When();

        [SetUp]
        public virtual void SetUp()
        {
            Task.Run(async () =>
            {
                Subject = await Given();
                await When();
            }).Wait();
        }
    }
}