using Contracts;
using Entities;

namespace Trade.Service.TrendIndicators
{
    internal class MaIndicator : ITrendIndicator<IList<DataBar>>
    {
        private int _period;
        private decimal _alphaRate;
        private readonly  ILogger _logger;

        public int WindowLength { get; set; }
        public int Period 
        { 
            get => _period;
            set
            {
                if (value < 1)
                    throw new ArgumentOutOfRangeException("Period must be greater than 1", nameof(Period));
                _period = value;
            }
        }

        public MaIndicator(ILogger logger, int period)
        {
            Period = period;
            _logger = logger;
            _alphaRate = 2 / (_period + 1);
        }

        public IList<DataBar> GetValue(IList<DataBar> data)
        {
            try
            {
                if (data == null)
                    throw new ArgumentNullException("Passed list of candlesticks equals null", nameof(data));

                if (data.Count == 0)
                    throw new ArgumentException("Passed list of candlesticks is empty", nameof(data));

                // The first EMA value is usually equal
                // to the price of the first value on the
                // candlestick chart
                data[0].EmaValue = data[0].ClosePrice;

                for (int currentItemIndex = 1; currentItemIndex < data.Count; currentItemIndex++)
                {
                    data[currentItemIndex].EmaValue = GetCurrentMaValue(
                        data[currentItemIndex].ClosePrice,
                        data[currentItemIndex - 1].EmaValue);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
            }

            return data;       
        }

        private decimal GetCurrentMaValue(decimal currentValue, decimal prevMaValue)
        {
            return _alphaRate * currentValue + (1 - _alphaRate) * prevMaValue;
        }
    }
}
