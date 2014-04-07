using System;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoNSubstitute;

namespace SpecificationFor
{
    public abstract class AutoSpecFor<T> : SpecFor<T>
    {
        private readonly Func<IFixture> _fixture;

        protected IFixture Fixture;

        protected AutoSpecFor()
            : this(() => new Fixture().Customize(new AutoNSubstituteCustomization()))
        {
        }

        protected AutoSpecFor(Func<IFixture> fixture)
        {
            _fixture = fixture;
        }

        [SetUp]
        public override void SetUp()
        {
            Fixture = _fixture();
            Subject = Given();
            When();
        }
    }
}