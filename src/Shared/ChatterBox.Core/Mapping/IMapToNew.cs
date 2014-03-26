namespace ChatterBox.Core.Mapping
{
    public interface IMapToNew<TSource, TTarget>
    {
        TTarget Map(TSource source);
    }
}