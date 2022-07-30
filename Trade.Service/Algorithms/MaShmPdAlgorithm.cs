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
    internal class MaShmPdAlgorithm : IAlgorithm<DataBarsWithDom>
    {
        private ILogger _logger;

        // Candlestick patterns
        private Hammer _hammer;
        private HangedMan _hangedMan;
        private ShootingStar _shootingStar;

        // Dom Indicators
        private PriceDensityIndicator _priceDensityIndicator;

        // Trend Indicators
        private MaIndicatorManager _maIndicatorManager;
        private SimpleColorIndicator _simpleColorIndicator;

        public MaShmPdAlgorithm(ILogger logger, Hammer hammer, HangedMan hangedMan, ShootingStar shootingStar,
            PriceDensityIndicator priceDensityIndicator, MaIndicatorManager maIndicatorManager, 
            SimpleColorIndicator simpleColorIndicator)
        {
            _logger = logger;
            _hammer = hammer;
            _hangedMan = hangedMan;
            _shootingStar = shootingStar;
            _priceDensityIndicator = priceDensityIndicator;
            _maIndicatorManager = maIndicatorManager;
            _simpleColorIndicator = simpleColorIndicator;
        }

        public MarketMovement GetMarketMovement(DataBarsWithDom data)
        {
            var result = MarketMovement.Undefined;

            try
            {
                var maIndicatorResult = _maIndicatorManager.GetValue(data.DataBars);
                var simpleColorIndicatorResult = _simpleColorIndicator.GetValue(data.DataBars);
                var hasMarketMovement = maIndicatorResult != MarketMovement.Undefined && 
                                        simpleColorIndicatorResult != MarketMovement.Undefined &&
                                        maIndicatorResult == simpleColorIndicatorResult;

                // If previous market trend was down
                // then check is there hammer pattern,
                // and analyze depth of market for asks
                // and bids
                if (hasMarketMovement && maIndicatorResult == MarketMovement.Down &&
                    simpleColorIndicatorResult == MarketMovement.Down)
                {
                    var hammerPatternResult = _hammer.GetValue(data.DataBars);
                    var priceDensityResult = _priceDensityIndicator.GetValue(data.DepthOfMarket);
                    
                    if (hammerPatternResult == MarketMovement.Up && priceDensityResult == MarketMovement.Up)
                        result = MarketMovement.Up;
                }

                // If previous market trend was up
                // then check is there hanged man 
                // pattern or shooting star, and 
                // analyze depth of market for asks
                // and bids
                if (hasMarketMovement && maIndicatorResult == MarketMovement.Up &&
                    simpleColorIndicatorResult == MarketMovement.Up)
                {
                    var hangedManResult = _hangedMan.GetValue(data.DataBars);
                    var shootingStarResult = _shootingStar.GetValue(data.DataBars);
                    var priceDensityResult = _priceDensityIndicator.GetValue(data.DepthOfMarket);

                    if ((hangedManResult == MarketMovement.Down || shootingStarResult == MarketMovement.Down) &&
                        priceDensityResult == MarketMovement.Down)
                        result = MarketMovement.Down;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
            }

            _logger.LogInformation($"\n<<\t{nameof(MaShmPdAlgorithm)}:\tMarket movement is {result}\t>>\n");

            return result;
        }
    }
}
