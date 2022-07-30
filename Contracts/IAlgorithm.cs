using Entities;

namespace Contracts
{
    public interface IAlgorithm<T>
    {
        public MarketMovement GetMarketMovement(T data);
    }
}
