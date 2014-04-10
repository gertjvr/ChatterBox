using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ChatterBox.ChatClient.Tests
{
    [TestFixture]
    public abstract class SpecificationForAsync<T>
        where T : class
    {
        protected T Subject;

        protected abstract Task<T> Given();

        protected abstract Task When();

        [SetUp]
        public virtual void SetUp()
        {
            var setup = Task.Run(async () =>
            {
                Subject = await Given();
                await When();
            });
            
            setup.Wait();
        }

        [TearDown]
        public virtual void TearDown()
        {
            var disposable = Subject as IDisposable;
            if (disposable != null) disposable.Dispose();
            Subject = null;
        }
    }
}