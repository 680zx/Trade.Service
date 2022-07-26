namespace Contracts
{
    public interface IIndicatorManager<T, K>
    {
        public T GetValue(K value);
    }
}
