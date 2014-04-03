using System;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoNSubstitute;

namespace ChatterBox.ChatServer.Tests
{
    public abstract class AutoSpecFor<T>
    {
        private readonly Func<IFixture> _fixture;

        protected IFixture Fixture;

        protected T Subject;

        protected AutoSpecFor()
            : this(() => new Fixture().Customize(new AutoNSubstituteCustomization()))
        {
        }

        protected AutoSpecFor(Func<IFixture> fixture)
        {
            _fixture = fixture;
        }

        protected abstract T Given();

        protected abstract void When();

        [SetUp]
        public void SetUp()
        {
            Fixture = _fixture();
            Subject = Given();
            When();
        }
    }
}