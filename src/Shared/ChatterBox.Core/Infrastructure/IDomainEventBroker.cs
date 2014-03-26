using ChatterBox.Core.Infrastructure.Facts;

namespace ChatterBox.Core.Infrastructure
{
    public interface IDomainEventBroker
    {
        void Raise<T>(T fact) where T : IFact;
    }
}