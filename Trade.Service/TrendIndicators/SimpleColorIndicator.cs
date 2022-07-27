using Contracts;
using Entities;

namespace Trade.Service.TrendIndicators
{
    internal class SimpleColorIndicator : ITrendIndicator<MarketMovement>
    {
        private readonly ILogger _logger;

        public int WindowLength { get; set; }

        public SimpleColorIndicator(ILogger logger)
        {
            _logger = logger;
        }

        public MarketMovement GetValue(IList<DataBar> value)
        {
            var result = MarketMovement.Undefined;

            try
            {
                var processData = value.TakeLast(WindowLength).ToList();
                var greenDataBarCount = 0;
                decimal greenDataBarTotalLength = 0;

                var redDataBarCount = 0;
                decimal redDataBarTotalLength = 0;
                
                for (int i = 0; i < processData.Count; i++)
                {
                    if (processData[i].Color == Color.Green)
                    {
                        greenDataBarTotalLength += processData[i].RealBody;
                        greenDataBarCount++;
                    }
                    else
                    {
                        redDataBarTotalLength += processData[i].RealBody;
                        redDataBarCount++;
                    }
                }

                if (greenDataBarCount > redDataBarCount && greenDataBarTotalLength > redDataBarTotalLength)
                {
                    result = MarketMovement.Up;
                }
                else if (greenDataBarCount < redDataBarCount && greenDataBarTotalLength < redDataBarTotalLength)
                {
                    result = MarketMovement.Down;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
            }

            _logger.LogInformation($"{nameof(SimpleColorIndicator)}:\tMarket trend is {result}");

            return result;
        }
    }
}
