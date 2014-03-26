namespace ChatterBox.Core.Infrastructure
{
    public interface IHandleFact<T>
    {
        void Handle(T domainEvent);
    }
}