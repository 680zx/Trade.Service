namespace Trade.Service.Settings.CandlestickPatternSettings
{
    internal class HammerSettings : BaseCandlestickPatternSettings
    {
        public HammerSettings(IConfiguration configuration) 
            : base(configuration, "Hammer")
        {
        }
    }
}
