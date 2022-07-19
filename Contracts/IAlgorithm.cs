using Entities;

namespace Contracts
{
    public interface IAlgorithm<T, K>
    {
        public T GetValue(K data);
    }
}