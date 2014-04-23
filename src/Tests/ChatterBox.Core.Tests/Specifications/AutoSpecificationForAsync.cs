using System;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoNSubstitute;

namespace ChatterBox.Core.Tests.Specifications
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

        protected IFixture Fixture { get; private set; }

        [SetUp]
        public override void SetUp()
        {
            Fixture = _fixture();

            base.SetUp();
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