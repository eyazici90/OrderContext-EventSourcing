namespace OrderContext.Domain.Shared
{
    public interface IPolicy<TResult, in T>
    {
        TResult Apply(T state);
    }

    public interface IPolicy<in T>
    {
        void Apply(T state);
    }
}
