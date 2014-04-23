namespace ChatterBox.Core.Tests.IoC.Conventions
{
    public enum Lifetime
    {
        Transient = 0,
        InstancePerLifetimeScope = 1,
        SingleInstance = 2,
        ExternallyOwned = 3,
        SingleInstanceExternallyOwned = 4
    }
}