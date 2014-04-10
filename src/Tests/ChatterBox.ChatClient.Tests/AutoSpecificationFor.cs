using System;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoNSubstitute;

namespace ChatterBox.ChatClient.Tests
{
    [TestFixture]
    public abstract class AutoSpecificationFor<T> : SpecificationFor<T>
        where T : class
    {
        private readonly Func<IFixture> _fixture;

        protected AutoSpecificationFor()
            : this(() => new Fixture().Customize(new AutoNSubstituteCustomization()))
        {
        }

        protected AutoSpecificationFor(Func<IFixture> fixture)
        {
            _fixture = fixture;
        }

        protected IFixture Fixture { get; private set; }

        [SetUp]
        public override void SetUp()
        {
            Fixture = _fixture();
            Subject = Given();
            When();
        }
    }
}