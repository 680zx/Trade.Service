using Contracts;
using Entities;

namespace Trade.Service.CandlestickPatterns
{
    internal class Hammer : ICandlestickPattern
    {
        public decimal MaxBodyLengthPercentage { get; set; }
        public decimal MinBodyLengthPercentage { get; set; }
        public decimal MaxUpperShadowLengthPercentage { get; set; }
        public decimal MaxLowerShadowLengthPercentage { get; set; }
        public int WindowLength { get; set; } = 5;

        public MarketMovement GetValue(IList<DataBar> data)
        {
            if (data == null)
                throw new ArgumentNullException("Passed data equals null");

            if (data.Count == 0)
                throw new ArgumentNullException("Passed data count is 0");

            var value = MarketMovement.Undefined;
            var midDataBarIndex = data.Count / 2;



            return value;
        }
    }
}
