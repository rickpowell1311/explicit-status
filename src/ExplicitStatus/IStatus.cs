namespace ExplicitStatus
{
    public interface IStatus<T, TStatus>
    {
        TStatus GetFor(T obj);
    }
}
