using System;
using System.Threading.Tasks;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoNSubstitute;

namespace ChatterBox.Core.Tests.Specifications
{
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

        protected IFixture Fixture { get; private set; }

        public override async Task SetUp()
        {
            Fixture = _fixture();
            Subject = await Given();
            await When();
        }

        public override async Task TearDown()
        {
            var disposable = Subject as IDisposable;
            if (disposable != null) disposable.Dispose();
            Subject = null;
            Fixture = null;
        }
    }
}