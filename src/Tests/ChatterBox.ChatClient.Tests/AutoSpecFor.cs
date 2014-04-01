using Ploeh.AutoFixture;

namespace ChatterBox.ChatClient.Tests
{
    public abstract class AutoSpecFor<T> : SpecFor<T>
    {
        protected readonly IFixture Fixture;

        protected AutoSpecFor()
            : this(new Fixture())
        {
   
        }

        protected AutoSpecFor(IFixture fixture)
        {
            Fixture = fixture;
        }
    }
}