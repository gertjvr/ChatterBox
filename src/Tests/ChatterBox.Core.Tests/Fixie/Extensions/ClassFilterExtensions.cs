using Fixie.Conventions;

namespace ChatterBox.Core.Tests.Fixie.Extensions
{
    public static class ClassFilterExtensions
    {
        public static ClassFilter NameStartsWith(this ClassFilter classFilter, string prefix)
        {
            return classFilter.Where(t => t.Name.StartsWith(prefix));
        }
    }
}