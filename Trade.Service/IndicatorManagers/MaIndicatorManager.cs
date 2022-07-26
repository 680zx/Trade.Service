using Contracts;
using Entities;
using Trade.Service.TrendIndicators;

namespace Trade.Service.IndicatorManagers
{
    internal class MaIndicatorManager : IIndicatorManager<MarketMovement, IList<DataBar>>
    {
        private readonly ILogger _logger;

        public int ShortPeriod { get; set; }
        public int LongPeriod { get; set; }
        public int WindowLength { get; set; }

        public MaIndicatorManager(ILogger logger)
        {
            _logger = logger;
        }

        public MarketMovement GetValue(IList<DataBar> data)
        {
            var result = MarketMovement.Undefined;

            try
            {
                var shortPeriodMaIndicator = new MaIndicator(_logger, ShortPeriod);
                var longPeriodMaIndicator = new MaIndicator(_logger, LongPeriod);

                var processData = data.TakeLast(WindowLength).ToList();
                var shortPeriodMaData = shortPeriodMaIndicator.GetValue(processData);
                var longPeriodMaData = longPeriodMaIndicator.GetValue(processData);

                if (shortPeriodMaData.Count != longPeriodMaData.Count)
                {
                    throw new Exception("The number of Short Period Ma data bar doesn't " +
                        "match the number of Long Period Ma data bar count");
                }

                var dataLength = shortPeriodMaData.Count;

                // Check market movement is down
                for (int i = 0; i < dataLength; i++)
                {
                    if (shortPeriodMaData[i].EmaValue < longPeriodMaData[i].EmaValue && i != dataLength)
                        result = MarketMovement.Down;
                    else
                        result = MarketMovement.Undefined;
                }

                // Check market movement is up
                for (int i = 0; i < dataLength; i++)
                {
                    if (longPeriodMaData[i].EmaValue < longPeriodMaData[i].EmaValue && i != dataLength)
                        result = MarketMovement.Up;
                    else
                        result = MarketMovement.Undefined;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
            }

            return result;
        }
    }
}
