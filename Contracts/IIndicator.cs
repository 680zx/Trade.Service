namespace Contracts
{
    public interface IIndicator<T, K>
    {
        public T GetValue(K value);
    }
}