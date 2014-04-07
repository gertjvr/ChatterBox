using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoNSubstitute;

namespace SpecificationFor
{
    public abstract class AutoAsyncSpecFor<T> : AsyncSpecFor<T>
    {
        private readonly Func<IFixture> _fixture;

        protected AutoAsyncSpecFor()
            : this(() => new Fixture().Customize(new AutoNSubstituteCustomization()))
        {
        }

        protected AutoAsyncSpecFor(Func<IFixture> fixture)
        {
            _fixture = fixture;
        }

        protected IFixture Fixture { get; set; }

        [SetUp]
        public override void SetUp()
        {
            Task.Run(async () =>
            {
                Fixture = _fixture();
                Subject = await Given();
                await When(); 
            });
        }
    }
}