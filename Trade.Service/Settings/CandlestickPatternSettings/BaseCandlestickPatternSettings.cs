using Contracts;

namespace Trade.Service.Settings.CandlestickPatternSettings
{
    internal abstract class BaseCandlestickPatternSettings : ICandlestickPatternSettings
    {
        public decimal MaxBodyLengthPercentage { get; set; }
        public decimal MinBodyLengthPercentage { get; set; }
        public decimal MaxUpperShadowLengthPercentage { get; set; }
        public decimal MinUpperShadowLengthPercentage { get; set; }
        public decimal MaxLowerShadowLengthPercentage { get; set; }
        public decimal MinLowerShadowLengthPercentage { get; set; }

        public BaseCandlestickPatternSettings(IConfiguration configuration, string candlestickPatternName)
        {
            configuration.GetSection(candlestickPatternName).Bind(this);
        }
    }
}
