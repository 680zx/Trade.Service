using Entities;

namespace Contracts
{
    public interface ICandlestickPattern : IIndicator<MarketMovement, IList<DataBar>>
    {
        public int WindowLength { get; set; }
    }
}