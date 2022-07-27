using Contracts;
using Entities;

namespace Trade.Service.CandlestickPatterns
{
    internal class ShootingStar : ICandlestickPattern
    {
        private readonly ILogger<ShootingStar> _logger;

        // What percentage is the candlestick body of the
        // larger shadow (upper/lower never mind).
        public decimal MaxBodyLengthPercentage { get; set; }

        public decimal MinBodyLengthPercentage { get; set; }
        public decimal MaxUpperShadowLengthPercentage { get; set; }
        public decimal MinUpperShadowLengthPercentage { get; set; }
        public decimal MaxLowerShadowLengthPercentage { get; set; }
        public decimal MinLowerShadowLengthPercentage { get; set; }
        public int WindowLength { get; set; }

        public ShootingStar(ILogger<ShootingStar> logger)
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

                result = HasPattern(data.Last()) ? MarketMovement.Down : MarketMovement.Undefined;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
            }

            _logger.LogInformation($"{nameof(ShootingStar)}:\tMarket movement is {result}");

            return result;
        }

        private bool HasPattern(DataBar data)
        {
            var shootingStarBodyToTotalLengthRatioPercentage = data.RealBody / data.TotalLength * 100;
            var shootingStarUpperShadowToTotalLengthRatioPercentage = data.UpperShadow / data.TotalLength * 100;
            var shootingStarLowerShadowToTotalLengthRatioPercentage = data.LowerShadow / data.TotalLength * 100;

            if (shootingStarBodyToTotalLengthRatioPercentage <= MaxBodyLengthPercentage &&
                shootingStarUpperShadowToTotalLengthRatioPercentage >= MaxUpperShadowLengthPercentage &&
                shootingStarLowerShadowToTotalLengthRatioPercentage <= MinLowerShadowLengthPercentage)
                return true;

            return false;
        }
    }
}
