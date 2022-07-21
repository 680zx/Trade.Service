using Contracts;
using Entities;

namespace Trade.Service.CandlestickPatterns
{
    internal class Hammer : ICandlestickPattern
    {
        // What percentage is the candlestick body of the
        // larger shadow (upper/lower never mind).
        public decimal MaxBodyLengthPercentage { get; set; }
        
        public decimal MinBodyLengthPercentage { get; set; }
        public decimal MaxUpperShadowLengthPercentage { get; set; }
        public decimal MaxLowerShadowLengthPercentage { get; set; }

        // The idea behind the Hammer pattern here
        // is to provide 5 candles where the first
        // 3 candlesticks is in front of the Hammer
        // and the last candlestick staying after the 
        // potential Hammer in the array should confirm
        // the pattern.
        // Nonetheless it can be setup by your own.
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
            var lastDataBarIndex = data.Count;

            // The potential hammer is in front
            // of the last candlestick in the array.
            var potentialHammerIndex = lastDataBarIndex - 1;

            if (data[potentialHammerIndex].RealBody / data[potentialHammerIndex].TotalLength < MaxBodyLengthPercentage &&
                )
                return true;

            return false;
        }
    }
}
