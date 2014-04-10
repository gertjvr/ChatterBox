using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoNSubstitute;

namespace ChatterBox.ChatServer.Tests
{
    [TestFixture]
    public abstract class AutoSpecificationForAsync<T> : SpecificationForAsync<T>
        where T : class
    {
        private readonly Func<IFixture> _fixture;

        protected AutoSpecificationForAsync()
            : this(() => new Fixture().Customize(new AutoNSubstituteCustomization()))
        {
        }

        protected AutoSpecificationForAsync(Func<IFixture> fixture)
        {
            _fixture = fixture;
        }

        protected IFixture Fixture;

        [SetUp]
        public override void SetUp()
        {
            var setup = Task.Run(async () =>
            {
                Fixture = _fixture();
                Subject = await Given();
                await When();
            });

            setup.Wait();
        }

        [TearDown]
        public override void TearDown()
        {
            var disposable = Subject as IDisposable;
            if (disposable != null) disposable.Dispose();
            Subject = null;
            Fixture = null;
        }
    }
}