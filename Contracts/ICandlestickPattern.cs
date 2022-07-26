using Entities;

namespace Contracts
{
    public interface ICandlestickPattern : IIndicator<MarketMovement, IList<DataBar>>
    {
        public decimal MaxBodyLengthPercentage { get; set; }
        public decimal MinBodyLengthPercentage { get; set; }
        public decimal MaxUpperShadowLengthPercentage { get; set; }
        public decimal MinUpperShadowLengthPercentage { get; set; }
        public decimal MaxLowerShadowLengthPercentage { get; set; }
        public decimal MinLowerShadowLengthPercentage { get; set; }
        public int WindowLength { get; set; }
    }
}