using Contracts;
using Entities;
using Trade.Service.CandlestickPatterns;
using Trade.Service.DomIndicators;
using Trade.Service.IndicatorManagers;
using Trade.Service.TrendIndicators;

namespace Trade.Service.Algorithms
{
    // MaShmPd
    // Ma   - moving average
    // S    - shooting star
    // h    - hammer
    // m    - hanged man
    // Pd   - price density
    internal class MaShmPdAlgorithm : IAlgorithm
    {
        // Candlestick patterns
        private Hammer _hammer;
        private HangedMan _hangedMan;
        private ShootingStar _shootingStar;

        // Dom Indicators
        private PriceDensityIndicator _priceDensityIndicator;

        // Trend Indicators
        private MaIndicatorManager _maIndicatorManager;
        private SimpleColorIndicator _simpleColorIndicator;

        public MaShmPdAlgorithm(Hammer hammer, HangedMan hangedMan, ShootingStar shootingStar,
            PriceDensityIndicator priceDensityIndicator, MaIndicatorManager maIndicatorManager, 
            SimpleColorIndicator simpleColorIndicator)
        {
            _hammer = hammer;
            _hangedMan = hangedMan;
            _shootingStar = shootingStar;
            _priceDensityIndicator = priceDensityIndicator;
            _maIndicatorManager = maIndicatorManager;
            _simpleColorIndicator = simpleColorIndicator;
        }

        public MarketMovement GetMarketMovement()
        {
            var result = MarketMovement.Undefined;

            try
            {

                var hasMarketMovement 
            }
            catch (Exception ex)
            {

            }

            return result;
        }
    }
}
