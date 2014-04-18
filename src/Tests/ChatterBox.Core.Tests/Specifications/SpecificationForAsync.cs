using System;
using System.Threading.Tasks;

namespace ChatterBox.Core.Tests.Specifications
{
    public abstract class SpecificationForAsync<T> : ISpecificationFor
        where T : class
    {
        protected T Subject;

        protected abstract Task<T> Given();

        protected abstract Task When();

        public virtual async Task SetUp()
        {
            Subject = await Given();
            await When();
        }

        public virtual async Task TearDown()
        {
            var disposable = Subject as IDisposable;
            if (disposable != null) disposable.Dispose();
            Subject = null;
        }
    }
}