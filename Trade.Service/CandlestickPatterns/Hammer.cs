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

            if (data.Count < WindowLength)
                throw new ArgumentNullException($"Passed data count is less then {nameof(WindowLength)}");

            var value = MarketMovement.Undefined;
            

            return value;
        }

        private bool HasPattern(IList<DataBar> data)
        {
            var midDataBarIndex = data.Count / 2;
            var prevDataBarIndex = midDataBarIndex--;
            var nextDataBarIndex = midDataBarIndex++;

            if (data[midDataBarIndex].RealBody / data[midDataBarIndex].TotalLength < MaxBodyLengthPercentage &&
                data[prevDataBarIndex].RealBody )
                return true;

            return false;
        }
    }
}
