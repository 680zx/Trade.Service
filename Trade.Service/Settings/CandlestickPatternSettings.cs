using Contracts;

namespace Trade.Service.Settings
{
    internal class CandlestickPatternSettings : ICandlestickPatternSettings
    {
        public decimal MaxBodyLengthPercentage { get; set; }
        public decimal MinBodyLengthPercentage { get; set; }
        public decimal MaxUpperShadowLengthPercentage { get; set; }
        public decimal MinUpperShadowLengthPercentage { get; set; }
        public decimal MaxLowerShadowLengthPercentage { get; set; }
        public decimal MinLowerShadowLengthPercentage { get; set; }

        public CandlestickPatternSettings(IConfiguration configuration, string jsonCandlestickPatternName)
        {
            configuration.GetSection(jsonCandlestickPatternName).Bind(this);
        }
    }
}
