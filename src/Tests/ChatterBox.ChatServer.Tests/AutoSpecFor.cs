using System;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace ChatterBox.ChatServer.Tests
{
    public abstract class AutoSpecFor<T>
    {
        private readonly Func<IFixture> _fixtureFactory;
        
        protected IFixture Fixture;

        protected AutoSpecFor(Func<IFixture> fixtureFactory)
        {
            _fixtureFactory = fixtureFactory;
        }

        protected T Subject;

        protected abstract T Given();

        protected abstract void When();

        [SetUp]
        public void SetUp()
        {
            Fixture = _fixtureFactory();

            Subject = Given();
            When();
        }
    }
}