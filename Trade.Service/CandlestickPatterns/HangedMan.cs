using Contracts;
using Entities;

namespace Trade.Service.CandlestickPatterns
{
    internal class HangedMan : ICandlestickPattern
    {
        ILogger<HangedMan> _logger;

        // What percentage is the candlestick body of the
        // larger shadow (upper/lower never mind).
        public decimal MaxBodyLengthPercentage { get; set; }

        public decimal MinBodyLengthPercentage { get; set; }
        public decimal MaxUpperShadowLengthPercentage { get; set; }
        public decimal MaxLowerShadowLengthPercentage { get; set; }
        public int WindowLength { get; set; }

        public HangedMan(ILogger<HangedMan> logger)
        {
            _logger = logger;
        }

        public MarketMovement GetValue(IList<DataBar> data)
        {
            var result = MarketMovement.Undefined;

            try
            {
                if (data == null)
                    throw new ArgumentNullException("Passed data equals null");

                result = HasPattern(data) ? MarketMovement.Down : MarketMovement.Undefined;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
            }

            return result;
        }

        private bool HasPattern(IList<DataBar> data)
        {
            // The last element in the list before Hanged
            // Man is used to validate the pattern.
            var validatingElement = data.Count;
            var hangedMan = validatingElement - 1;

            var hangedManBodyToTotalLengthRatio = data[hangedMan].RealBody / data[hangedMan].TotalLength * 100;
            var hangedManUpperShadowToTotalLengthRatio = data[hangedMan].UpperShadow / data[hangedMan].TotalLength * 100;
            var hangedManLowerShadowToTotalLengthRatio = data[hangedMan].LowerShadow / data[hangedMan].TotalLength * 100;

            if (hangedManBodyToTotalLengthRatio < MaxBodyLengthPercentage &&
                hangedManUpperShadowToTotalLengthRatio < MaxUpperShadowLengthPercentage &&
                hangedManLowerShadowToTotalLengthRatio < MaxLowerShadowLengthPercentage &&
                data[validatingElement].ClosePrice < data[hangedMan].ClosePrice)
                return true;

            return false;
        }
    }
}
